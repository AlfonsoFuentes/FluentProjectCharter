using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.Scopes.Responses;

namespace FluentWeb.Pages.Scopes;
#nullable disable
public partial class ScopeTabs
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public ScopeResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    private async Task HandleOnTabChange(FluentTab tab)
    {
        if (App.Project == null || App.Project.CurrentCase == null || App.Project.CurrentCase.CurrentScope == null) return;
        if (App.Project.CurrentCase.CurrentScope != null)
        {
            App.Project.CurrentCase.CurrentScope.Tab = tab.Id;
            await GenericService.UpdateState(App.Project.CurrentCase.CurrentScope);
        }

        StateHasChanged();

    }
}
