using Microsoft.AspNetCore.Components;
using Shared.Models.MeetingAgreements.Requests;
#nullable disable
namespace FluentWeb.Pages.MeetingAgreements;
public partial class CreateMeetingAgreement
{
    CreateMeetingAgreementRequest Model = new();
    [Parameter]
    public Guid MeetingId { get; set; }
    [Parameter]
    public Guid ProjectId { get; set; }
    protected override void OnInitialized()
    {
        Model.MeetingId = MeetingId;
        Model.ProjectId = ProjectId;

    }
  
}
