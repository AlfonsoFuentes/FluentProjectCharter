using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Models.Cases.Responses;

namespace FluentWeb.Pages.BusinessCases;
#nullable disable
public partial class BusinessCasesTab
{
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    [CascadingParameter]
    public App App { get; set; }
    private async Task HandleOnTabChange(FluentTab tab)
    {
        if (App.Project == null) return;
        if (App.Project.CurrentCase != null)
        {
            App.Project.CurrentCase.Tab = tab.Id;
            await GenericService.UpdateState(App.Project.CurrentCase);
        }


        StateHasChanged();
    }
    [Parameter]
    [EditorRequired]
    public CaseResponse Parent { get; set; } = new();

}
