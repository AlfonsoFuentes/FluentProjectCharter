using Toolbelt.Blazor;
using Web.Infrastructures.Identity.Account;

namespace Web.Infrastructures.Interceptor
{
    public interface IHttpInterceptorManager : IManagetAuth
    {
        void RegisterEvent();

        Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e);

        void DisposeEvent();
    }
}