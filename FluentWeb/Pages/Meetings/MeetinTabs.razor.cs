using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.Meetings.Responses;
#nullable disable
namespace FluentWeb.Pages.Meetings;
public partial class MeetinTabs
{
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    [CascadingParameter]
    public App App { get; set; }
    private async Task HandleOnTabChange(FluentTab tab)
    {
        if (App.ProjectList.CurrentProject == null) return;
        if (App.ProjectList.CurrentProject.CurrentMeeting != null)
        {
            App.ProjectList.CurrentProject.CurrentMeeting.Tab = tab.Id;
            await GenericService.UpdateState(App.ProjectList.CurrentProject.CurrentMeeting);
        }

        StateHasChanged();
    }
    [Parameter]
    [EditorRequired]
    public MeetingResponse Parent { get; set; } = new();

}
