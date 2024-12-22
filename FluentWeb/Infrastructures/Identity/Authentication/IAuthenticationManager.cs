using FluentWeb.Infrastructures.Identity.Account;

namespace FluentWeb.Infrastructures.Identity.Authentication
{
    public interface IAuthenticationManager : IManagetAuth
    {
        Task<IResult> Login(TokenRequest model);

        Task<IResult> Logout();

        Task<string> RefreshToken();

        Task<string> TryRefreshToken();

        Task<string> TryForceRefreshToken();

      
    }
}