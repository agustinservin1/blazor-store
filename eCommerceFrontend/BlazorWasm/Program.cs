using BlazorWasm;
using BlazorWasm.Authentication;
using ClientLibrary.Helper;
using ClientLibrary.Helper.Implementations;
using ClientLibrary.Helper.Interfaces;
using ClientLibrary.Services.Implementations;
using ClientLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NetcodeHub.Packages.WebAssembly.Storage.Cookie;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddNetcodeHubCookieStorageService();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddScoped<IApiCallHelper, ApiCallHelper>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddHttpClient(Constant.ApiClient.Name, option =>
{
    option.BaseAddress = new Uri("https://localhost:7267/api/");
}
);


await builder.Build().RunAsync();
