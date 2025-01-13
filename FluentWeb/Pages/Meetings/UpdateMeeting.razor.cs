using Microsoft.AspNetCore.Components;
using Shared.Enums.Meetings;
using Shared.Models.Meetings.Records;
using Shared.Models.Meetings.Requests;
using Shared.Models.Meetings.Responses;
#nullable disable
namespace FluentWeb.Pages.Meetings;
public partial class UpdateMeeting
{
    UpdateMeetingRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<MeetingResponse, GetMeetingByIdRequest>(new GetMeetingByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = ProjectId,
                DateofMeeting = result.Data.DateofMeeting,
                MeetingType = MeetingTypeEnum.GetType(result.Data.MeetingType),
                Subject = result.Data.Subject,
                  
            };
        }
    }

}
