using Blazored.FluentValidation;

namespace FluentWeb.Pages.Authentication;
public partial class Register
{
    private bool Validated { get; set; }
    FluentValidationValidator _fluentValidationValidator = null!;
    async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }
    private RegisterRequest _registerUserModel = new();

    private async Task SubmitAsync()
    {
        var response = await _userManager.RegisterUserAsync(_registerUserModel);
        if (response.Succeeded)
        {
            _snackBar.ShowSuccess(response.Messages);
            _NavigationManager.NavigateTo("/login");
            _registerUserModel = new RegisterRequest();
        }
        else
        {
            _snackBar.ShowSuccess(response.Messages);

        }
    }
}
