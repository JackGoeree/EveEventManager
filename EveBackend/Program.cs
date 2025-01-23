using Microsoft.EntityFrameworkCore;
using EveBackend.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<EveBackendDbContext>(options =>
    options.UseSqlite("Data Source=EveBackend.db"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = MicrosoftAccountDefaults.AuthenticationScheme;
})
.AddCookie()
.AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
    microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
    microsoftOptions.CallbackPath = "/signin-microsoft";

    microsoftOptions.Scope.Add("openid");
    microsoftOptions.Scope.Add("email");
    microsoftOptions.Scope.Add("profile");

    microsoftOptions.SaveTokens = true;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5216") // Frontend origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/auth/login", async context =>
{
    await context.ChallengeAsync(MicrosoftAccountDefaults.AuthenticationScheme, new AuthenticationProperties
    {
        RedirectUri = "http://localhost:5066/api/auth/loginsuccess"
    });
});

app.MapGet("/api/auth/loginsuccess", async context =>
{
    var user = context.User;

    if (user.Identity?.IsAuthenticated == true)
    {
        var email = user.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
        var name = user.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

        context.Response.Redirect($"http://localhost:5216/userinfo?name={Uri.EscapeDataString(name)}&email={Uri.EscapeDataString(email)}");
    }
    else
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Not authenticated");
    }
});

app.MapGet("/api/auth/logout", async context =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    context.Response.Redirect("/");
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.MapControllers();

app.Run();
