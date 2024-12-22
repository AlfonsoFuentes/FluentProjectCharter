using Microsoft.AspNetCore.Components;
using Shared.Models.AppStates.Responses;
using Web.Infrastructure.Services.Currencies;

namespace FluentWeb;
#nullable disable
public partial class App
{
    public ClaimsPrincipal CurrentUser { get; set; }
    [Inject]
    public IRate _CurrencyService { get; set; }
    public ConversionRate RateList { get; set; }
    public ActiveAppResponse AppState { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {

        RateList = await _CurrencyService.GetRates(DateTime.UtcNow);



    }
    public async Task GetActiveProject()
    {
        var result = await AppStateService.GetProjectState(AppState);
        if (result.Succeeded)
        {
            AppState = result.Data;
        }
    }
    public async Task GetCurrentUser()
    {
        var state = await _stateProvider.GetAuthenticationStateAsync();
        if (state.User.Identity.IsAuthenticated && state != new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())))
        {
            CurrentUser = state.User;
            var result = await AppStateService.GetInitialProjectState();
            if (result.Succeeded)
            {
                AppState = result.Data;
                await GetActiveProject();
                StateHasChanged();
            }
        }
    }
    public string Email => CurrentUser == null ? string.Empty : CurrentUser.FindFirstValue(ClaimTypes.Email)!;
    public string Name => CurrentUser == null ? string.Empty : CurrentUser.FindFirstValue(ClaimTypes.Name)!;
    public string SurName => CurrentUser == null ? string.Empty : CurrentUser.FindFirstValue(ClaimTypes.Surname)!;
    public char FN => CurrentUser == null || Name == null ? ' ' : Name.Length == 0 ? ' ' : Name[0];
    public char FSN => CurrentUser == null || SurName == null ? ' ' : SurName.Length == 0 ? ' ' : SurName[0];
    public string Initials => CurrentUser == null ? string.Empty : $"{FN}{FSN}";
    public string FullName => CurrentUser == null ? string.Empty : $"{Name} {SurName}";

}