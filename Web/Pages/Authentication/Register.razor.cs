using Blazored.FluentValidation;
using Radzen;

namespace Web.Pages.Authentication;
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
            _snackBar.Add(response.Messages, NotificationSeverity.Success);
            _NavigationManager.NavigateTo("/login");
            _registerUserModel = new RegisterRequest();
        }
        else
        {
            _snackBar.Add(response.Messages, NotificationSeverity.Error);
           
        }
    }

}
