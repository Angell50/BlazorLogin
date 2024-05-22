using BlazorLogin.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorLogin.Client.Extensiones;
using Blazored.Toast;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider,AutenticacionExtension>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredToast();


await builder.Build().RunAsync();
