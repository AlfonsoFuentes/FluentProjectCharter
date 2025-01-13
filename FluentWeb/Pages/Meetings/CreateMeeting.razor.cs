using Microsoft.AspNetCore.Components;
using Shared.Models.Meetings.Requests;
#nullable disable
namespace FluentWeb.Pages.Meetings;
public partial class CreateMeeting
{
    CreateMeetingRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }

    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;

    }
  
}
