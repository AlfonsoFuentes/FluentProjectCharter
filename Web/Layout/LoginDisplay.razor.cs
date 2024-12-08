using Microsoft.AspNetCore.Components;
#nullable disable
namespace Web.Layout;
public partial class LoginDisplay
{
    [CascadingParameter]
    public App MainApp { get; set; }

    ClaimsPrincipal _currentUser = null!;

    protected override async Task OnParametersSetAsync()
    {
        _currentUser = await _authenticationManager.CurrentUser();

    }

    string Email => _currentUser == null ? string.Empty : _currentUser.FindFirstValue(ClaimTypes.Email)!;
    string Name => _currentUser == null ? string.Empty : _currentUser.FindFirstValue(ClaimTypes.Name)!;
    string SurName => _currentUser == null ? string.Empty : _currentUser.FindFirstValue(ClaimTypes.Surname)!;
    char FN => _currentUser == null || Name == null ? ' ' : Name.Length == 0 ? ' ' : Name[0];
    char FSN => _currentUser == null || SurName == null ? ' ' : SurName.Length == 0 ? ' ' : SurName[0];
    string Initials => _currentUser == null ? string.Empty : $"{FN}{FSN}";
    string FullName => _currentUser == null ? string.Empty : $"{Name} {SurName}";

}
