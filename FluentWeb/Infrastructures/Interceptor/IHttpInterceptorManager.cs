using FluentWeb.Infrastructures.Identity.Account;
using Toolbelt.Blazor;

namespace FluentWeb.Infrastructures.Interceptor
{
    public interface IHttpInterceptorManager : IManagetAuth
    {
        void RegisterEvent();

        Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e);

        void DisposeEvent();
    }
}