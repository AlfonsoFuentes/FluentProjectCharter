using BlazorDownloadFile;
using FluentWeb.RegisterServices;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using UnitSystem;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder
.AddRootComponents()
.AddClientServices();
builder.Services.AddSingleton<NavigationBack>();
builder.Services.AddBlazorDownloadFile();
UnitManager.RegisterByAssembly(typeof(SIUnitTypes).Assembly);

var host = builder.Build();
host.Services.GetService<NavigationBack>();
await host.RunAsync();
