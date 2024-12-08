using Microsoft.AspNetCore.Components;
using Radzen;
using Toolbelt.Blazor;
using Web.Infrastructures.Identity.Authentication;
using Web.Infrastructures.NotificacionService;

namespace Web.Infrastructures.Interceptor
{
    public class HttpInterceptorManager : IHttpInterceptorManager
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly NavigationManager _navigationManager;
        private readonly ISnackBar _snackBar;


        public HttpInterceptorManager(
            HttpClientInterceptor interceptor,
            IAuthenticationManager authenticationManager,
            NavigationManager navigationManager,
            ISnackBar snackBar
)
        {
            _interceptor = interceptor;
            _authenticationManager = authenticationManager;
            _navigationManager = navigationManager;
            _snackBar = snackBar;

        }

        public void RegisterEvent() => _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;

        public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            var absPath = e.Request.RequestUri!.AbsolutePath;
            if (!absPath.Contains("token") && !absPath.Contains("accounts"))
            {
                try
                {
                    var token = await _authenticationManager.TryRefreshToken();
                    if (!string.IsNullOrEmpty(token))
                    {
                        _snackBar.Add("Refreshed Token.", NotificationSeverity.Error);
                        e.Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _snackBar.Add("You are Logged Out.", NotificationSeverity.Error);
                    await _authenticationManager.Logout();
                    _navigationManager.NavigateTo("/");
                }
            }
        }

        public void DisposeEvent() => _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
    }
}