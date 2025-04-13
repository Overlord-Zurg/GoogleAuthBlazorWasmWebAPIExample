using BlazorWebAppAutoNoneAuth.Client.Pages;
using BlazorWebAppAutoNoneAuth.Components;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var googleClientID =
    builder.Configuration["Authentication:Google:development-examples:ClientId"]!;
var googleClientSecret =
    builder.Configuration["Authentication:Google:development-examples:ClientSecret"]!;

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthentication().AddGoogle(o =>
{
    o.ClientId = googleClientID;
    o.ClientSecret = googleClientSecret;
    //o.CallbackPath = "/signin-google";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseCookiePolicy();
app.UseAuthentication();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorWebAppAutoNoneAuth.Client._Imports).Assembly);

app.MapRazorPages(); // for login/logout pages

app.Run();
