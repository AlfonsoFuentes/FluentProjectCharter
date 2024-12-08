﻿using Web.Infrastructures.Identity.Account;

namespace Web.Infrastructures.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManagetAuth
    {
        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimRequest role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}