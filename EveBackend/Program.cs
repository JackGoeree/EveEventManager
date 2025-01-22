using Microsoft.EntityFrameworkCore;
using EveBackend.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/auth/login", async context =>
{
    await context.ChallengeAsync(MicrosoftAccountDefaults.AuthenticationScheme, new AuthenticationProperties
    {
        RedirectUri = "/api/auth/loginsuccess"
    });
});

app.MapGet("/api/auth/loginsuccess", async context =>
{
    var user = context.User;

    if (user.Identity?.IsAuthenticated == true)
    {
        var email = user.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
        var name = user.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

        await context.Response.WriteAsJsonAsync(new { Name = name, Email = email });
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.MapControllers();

app.Run();
