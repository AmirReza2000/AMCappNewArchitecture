using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Blazored.Toast;
using Client;
using Client.Infrastructure.Providers;
using Client.Services;
using Client.Services.Contracts;
using Constants;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddBlazoredSessionStorage();

builder.Services.AddAuthorizationCore();

builder.Services.AddBlazoredToast();

builder.Services.AddScoped
    <CustomAuthenticationStateProvider>();

builder.Services.AddScoped
    <Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider>
    (current => current.GetRequiredService<CustomAuthenticationStateProvider>());

builder.Services.AddScoped<
    IAccountRepository,AccountRepository>();

builder.Services.AddSingleton<LogsService>();

builder.Services.AddScoped
    (implementationFactory: current => new System.Net.Http.HttpClient
    {
        BaseAddress = new System.Uri
            (uriString: CommonRouting.BaseApiUrl),
    });
await builder.Build().RunAsync();
