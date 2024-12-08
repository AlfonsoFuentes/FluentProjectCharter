﻿using FluentWeb.Infrastructures.Identity.Account;

namespace FluentWeb.Infrastructures.Identity.Roles
{
    public interface IRoleManager : IManagetAuth
    {
        Task<IResult<List<RoleResponse>>> GetRolesAsync();

        Task<IResult<string>> SaveAsync(RoleRequest role);

        Task<IResult<string>> DeleteAsync(string id);

        Task<IResult<PermissionResponse>> GetPermissionsAsync(string roleId);

        Task<IResult<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}