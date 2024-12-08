using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Radzen;
#nullable disable
namespace Web.Pages.Authentication;
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
    private TokenRequest _tokenModel = new();

    protected override async Task OnInitializedAsync()
    {
        await ValidateAsync();
        var state = await _stateProvider.GetAuthenticationStateAsync();
        if (state != new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())))
        {

            _NavigationManager.NavigateTo("/");
        }
    }

    private async Task SubmitAsync()
    {
        var result = await _authenticationManager.Login(_tokenModel);
        if (!result.Succeeded)
        {
            _snackBar.Add(result.Messages, NotificationSeverity.Error);
        }
        
    }
}
