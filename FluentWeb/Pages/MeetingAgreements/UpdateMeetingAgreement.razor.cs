using Microsoft.AspNetCore.Components;
using Shared.Models.MeetingAgreements.Records;
using Shared.Models.MeetingAgreements.Requests;
using Shared.Models.MeetingAgreements.Responses;
#nullable disable
namespace FluentWeb.Pages.MeetingAgreements;
public partial class UpdateMeetingAgreement
{
    UpdateMeetingAgreementRequest Model = new();

    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<MeetingAgreementResponse, GetMeetingAgreementByIdRequest>(new GetMeetingAgreementByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = result.Data.ProjectId,
                MeetingId = result.Data.MeetingId,
                  
            };
        }
    }

}
