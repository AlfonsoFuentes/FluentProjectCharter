using FluentWeb.Infrastructures.Identity.Account;

namespace FluentWeb.Infrastructures.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManagetAuth
    {
        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimRequest role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}