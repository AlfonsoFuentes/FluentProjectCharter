using Web.Infrastructures.Identity.Account;

namespace Web.Infrastructures.Identity.Authentication
{
    public interface IAuthenticationManager : IManagetAuth
    {
        Task<IResult> Login(TokenRequest model);

        Task<IResult> Logout();

        Task<string> RefreshToken();

        Task<string> TryRefreshToken();

        Task<string> TryForceRefreshToken();

        Task<ClaimsPrincipal> CurrentUser();
    }
}