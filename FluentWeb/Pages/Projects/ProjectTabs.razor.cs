using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Models.Projects.Reponses;

namespace FluentWeb.Pages.Projects;
#nullable disable
public partial class ProjectTabs
{
    [CascadingParameter]
    public App App { get; set; }
    private async Task HandleOnTabChange(FluentTab tab)
    {
        if (App.ProjectList.CurrentProject != null)
        {
            App.ProjectList.CurrentProject.Tab = tab.Id;
            await GenericService.UpdateState(App.ProjectList.CurrentProject);
        }

        StateHasChanged();
    }
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    [Parameter]
    [EditorRequired]
    public ProjectResponse Parent { get; set; } = new();

}
