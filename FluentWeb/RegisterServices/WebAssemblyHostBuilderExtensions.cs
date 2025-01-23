using FluentWeb.Authentications;
using FluentWeb.Infrastructures.Identity.Account;
using FluentWeb.Infrastructures.NotificacionService;
using FluentWeb.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Components.Tooltip;
using System.Globalization;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Web.Infrastructure.Managers;
using Web.Infrastructure.Services.Client;
using Web.Infrastructure.Services.Currencies;



namespace FluentWeb.RegisterServices
{

    public static class WebAssemblyHostBuilderExtensions
    {
        public static string ClientName = "API";
        public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#app");

            return builder;
        }

        public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
        {
            builder
                .Services

                .AddAuthorizationCore()
                .AddBlazoredLocalStorage()

                .AddFluentUIComponents()
                .AddScoped<BlazorHeroStateProvider>()
                .AddScoped<AuthenticationStateProvider, BlazorHeroStateProvider>()
                .AddManagers()
                .AddManagerAuth()
                .AddTransient<AuthenticationHeaderHandler>()
                .AddScoped(sp => sp
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient(ClientName).EnableIntercept(sp))
                .AddHttpClient(ClientName, client =>
                {
                    client.DefaultRequestHeaders.AcceptLanguage.Clear();
                    client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(CultureInfo.DefaultThreadCurrentCulture?.TwoLetterISOLanguageName);
                    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                })
                .AddHttpMessageHandler<AuthenticationHeaderHandler>();
            builder.Services.AddHttpClientInterceptor();
            builder.Services.AddScoped<IHttpClientService, HttpClientService>();
            builder.Services.AddScoped<ITooltipService, TooltipService>();
            builder.Services.CurrencyService();
            builder.Services.AddScoped<ISnackBar, SnackBar>();
            builder.Services.AddScoped<IModelLocalStorage, ModelLocalStorage>();

            return builder;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            var managers = typeof(IManager);

            var types = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null).ToList();

            foreach (var type in types)
            {
                if (managers.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
            }

            return services;
        }

        public static IServiceCollection AddManagerAuth(this IServiceCollection services)
        {
            var managers = typeof(IManagetAuth);

            var types = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null).ToList();

            foreach (var type in types)
            {
                if (managers.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
            }

            return services;
        }

    }
}
