using BlazorDownloadFile;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web.RegisterServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder
.AddRootComponents()
.AddClientServices();
builder.Services.AddSingleton<NavigationBack>();
builder.Services.AddBlazorDownloadFile();

var host = builder.Build();
host.Services.GetService<NavigationBack>();
await host.RunAsync();
