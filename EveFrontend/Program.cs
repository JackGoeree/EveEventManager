using EveFrontend.Components;
using EveFrontend.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5066/");
});
builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5066/");
});
builder.Services.AddHttpClient<EventService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5066/");
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddMudServices();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "Microsoft";
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
    microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
});

builder.Services.AddAuthorization();

builder.Services.AddSingleton<UserState>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
