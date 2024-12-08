using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;

namespace FluentWeb.Pages.Authentication;
#nullable disable
public partial class Login
{
    [CascadingParameter]
    public App App { get; set; }
    private bool Validated { get; set; }
    FluentValidationValidator _fluentValidationValidator = null!;
    async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }
    private TokenRequest _registerUserModel = new();

    protected override async Task OnInitializedAsync()
    {
     
        var state = await _stateProvider.GetAuthenticationStateAsync();
        if (state != new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())))
        {

            _NavigationManager.NavigateTo("/");
        }
    }

    private async Task SubmitAsync()
    {
        var result = await _authenticationManager.Login(_registerUserModel);
        if (!result.Succeeded)
        {
            _snackBar.ShowError(result.Messages);
        }

    }
}
