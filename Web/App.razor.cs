using Microsoft.AspNetCore.Components;
using Radzen;

namespace Web;
#nullable disable
public partial class App
{
 
    public ClaimsPrincipal _currentUser;
    public async Task AutheticateUserAccess()
    {
        _currentUser = await _authenticationManager.CurrentUser();




    }
    public void ShowTooltip(ElementReference elementReference, string ToolTip) =>
    TooltipService.Open(elementReference, ToolTip, new TooltipOptions() { Delay = 500, Duration = 5000 });
}

