﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Server.Database.Identity;
using Server.Exceptions;
using Server.ExtensionsMethods;
using Server.Implementations.Identity.Specifications;
using Server.Interfaces.Identity;
using Server.Interfaces.Storage;
using Server.Interfaces.UserServices;
using Shared.Constants.Role;
using Shared.Models.IdentityModels.Requests.Identity;
using Shared.Models.IdentityModels.Requests.Mail;
using Shared.Models.IdentityModels.Responses.Identity;
using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;


namespace Server.Implementations.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<BlazorHeroUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IMailService _mailService;

        private readonly IExcelService _excelService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<BlazorHeroUser> userManager,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager,
            //IMailService mailService,

            IExcelService excelService,
            ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            //_mailService = mailService;

            _excelService = excelService;
            _currentUserService = currentUserService;
        }

        public async Task<Result<List<UserResponse>>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var result = _mapper.Map<List<UserResponse>>(users);
            return await Result<List<UserResponse>>.SuccessAsync(result);
        }

        public async Task<IResult> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                return await Result.FailAsync(string.Format("Username {0} is already taken.", request.UserName));
            }
            var user = new BlazorHeroUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                IsActive = true,
                EmailConfirmed = true
            };

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                var userWithSamePhoneNumber = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
                if (userWithSamePhoneNumber != null)
                {
                    return await Result.FailAsync(string.Format("Phone number {0} is already registered.", request.PhoneNumber));
                }
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                try
                {
                    var result = await _userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        var role = await _roleManager.FindByNameAsync(RoleConstants.BasicRole);
                        
                        await _userManager.AddToRoleAsync(user, RoleConstants.BasicRole);

                        return await Result<string>.SuccessAsync(user.Id, string.Format("User {0} Registered.", user.UserName));
                    }
                    else
                    {
                        return await Result.FailAsync(result.Errors.Select(a => a.Description).ToList());
                    }
                }
                catch (Exception ex) {

                    string exm = ex.Message;
                    return await Result.FailAsync(exm);
                }

              
            }
            else
            {
                return await Result.FailAsync(string.Format("Email {0} is already registered.", request.Email));
            }
        }

        private async Task<string> SendVerificationEmail(BlazorHeroUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/identity/user/confirm-email/";
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            return verificationUri;
        }

        public async Task<IResult<UserResponse>> GetAsync(string userId)
        {
            var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var result = _mapper.Map<UserResponse>(user);
            return await Result<UserResponse>.SuccessAsync(result);
        }

        public async Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request)
        {
            var user = await _userManager.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync();
            var isAdmin = await _userManager.IsInRoleAsync(user!, RoleConstants.AdministratorRole);
            if (isAdmin)
            {
                return await Result.FailAsync("Administrators Profile's Status cannot be toggled");
            }
            if (user != null)
            {
                user.IsActive = request.ActivateUser;
                user.EmailConfirmed = request.ActivateUser;
                var identityResult = await _userManager.UpdateAsync(user);
            }
            return await Result.SuccessAsync();
        }

        public async Task<IResult<UserRolesResponse>> GetRolesAsync(string userId)
        {
            var viewModel = new List<UserRoleModel>();
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _roleManager.Roles.ToListAsync();

            foreach (var role in roles)
            {
                var userRolesViewModel = new UserRoleModel
                {
                    RoleName = role.Name!,
                  
                };
                if (await _userManager.IsInRoleAsync(user!, role.Name!))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }
            var result = new UserRolesResponse { UserRoles = viewModel };
            return await Result<UserRolesResponse>.SuccessAsync(result);
        }

        public async Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user!.Email == "mukesh@blazorhero.com")
            {
                return await Result.FailAsync("Not Allowed.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var selectedRoles = request.UserRoles.Where(x => x.Selected).ToList();

            var currentUser = await _userManager.FindByIdAsync(_currentUserService.UserId);
            if (!await _userManager.IsInRoleAsync(currentUser!, RoleConstants.AdministratorRole))
            {
                var tryToAddAdministratorRole = selectedRoles
                    .Any(x => x.RoleName == RoleConstants.AdministratorRole);
                var userHasAdministratorRole = roles.Any(x => x == RoleConstants.AdministratorRole);
                if (tryToAddAdministratorRole && !userHasAdministratorRole || !tryToAddAdministratorRole && userHasAdministratorRole)
                {
                    return await Result.FailAsync("Not Allowed to add or delete Administrator Role if you have not this role.");
                }
            }

            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, selectedRoles.Select(y => y.RoleName));
            return await Result.SuccessAsync("Roles Updated");
        }

        public async Task<IResult<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user!, code);
            if (result.Succeeded)
            {
                return await Result<string>.SuccessAsync(user!.Id, string.Format("Account Confirmed for {0}. You can now use the /api/identity/token endpoint to generate JWT.", user.Email));
            }
            else
            {
                throw new ApiException(string.Format("An error occurred while confirming {0}", user!.Email));
            }
        }

        public async Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return await Result.FailAsync("An Error has occurred!");
            }
            // For more information on how to enable account confirmation and password reset please
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "account/reset-password";
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            var passwordResetURL = QueryHelpers.AddQueryString(endpointUri.ToString(), "Token", code);
            var mailRequest = new MailRequest
            {
                Body = string.Format("Please reset your password by <a href='{0}'>clicking here</a>.", HtmlEncoder.Default.Encode(passwordResetURL)),
                Subject = "Reset Password",
                To = request.Email
            };
            //BackgroundJob.Enqueue(() => _mailService.SendAsync(mailRequest));
            return await Result.SuccessAsync("Password Reset Mail has been sent to your authorized Email.");
        }

        public async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return await Result.FailAsync("An Error has occured!");
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (result.Succeeded)
            {
                return await Result.SuccessAsync("Password Reset Successful!");
            }
            else
            {
                return await Result.FailAsync("An Error has occured!");
            }
        }

        public async Task<int> GetCountAsync()
        {
            var count = await _userManager.Users.CountAsync();
            return count;
        }

        public async Task<string> ExportToExcelAsync(string searchString = "")
        {
            var userSpec = new UserFilterSpecification(searchString);
            var users = await _userManager.Users
                .Specify(userSpec)
                .OrderByDescending(a => a.CreatedOn)
                .ToListAsync();
            var result = await _excelService.ExportAsync(users, sheetName: "Users",
                mappers: new Dictionary<string, Func<BlazorHeroUser, object>>
                {
                    { "Id", item => item.Id },
                    { "FirstName", item => item.FirstName! },
                    { "LastName", item => item.LastName! },
                    { "UserName", item => item.UserName! },
                    { "Email", item => item.Email! },
                    { "EmailConfirmed", item => item.EmailConfirmed },
                    { "PhoneNumber", item => item.PhoneNumber! },
                    { "PhoneNumberConfirmed", item => item.PhoneNumberConfirmed },
                    { "IsActive", item => item.IsActive },
                    { "CreatedOn (Local)", item => DateTime.SpecifyKind(item.CreatedOn, DateTimeKind.Utc).ToLocalTime().ToString("G", CultureInfo.CurrentCulture) },
                    { "CreatedOn (UTC)", item => item.CreatedOn.ToString("G", CultureInfo.CurrentCulture) },
                    { "ProfilePictureDataUrl", item => item.ProfilePictureDataUrl! },
                });

            return result;
        }
    }
}