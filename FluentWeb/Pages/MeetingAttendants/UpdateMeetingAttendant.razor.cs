using Microsoft.AspNetCore.Components;
using Shared.Models.MeetingAttendants.Records;
using Shared.Models.MeetingAttendants.Requests;
using Shared.Models.MeetingAttendants.Responses;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.StakeHolders;
#nullable disable
namespace FluentWeb.Pages.MeetingAttendants;
public partial class UpdateMeetingAttendant
{
    UpdateMeetingAttendantRequest Model = new();

    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await UpdateStakeHolder();

        var result = await GenericService.GetById<MeetingAttendantResponse, GetMeetingAttendantByIdRequest>(new GetMeetingAttendantByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                ProjectId = result.Data.ProjectId,
                MeetingId = result.Data.MeetingId,
                StakeHolder=result.Data.StakeHolder,
                  
            };
            stakeholder=Model.StakeHolder.Name;
        }
    }
    void AddStakeHolder()
    {
        Navigation.NavigateTo($"/CreateStakeHolder");
    }
    [Inject]
    private IStakeHolderService StakeHolderService { get; set; }
    StakeHolderResponseList StakeHolderResponseList = new();

    async Task UpdateStakeHolder()
    {
        var result = await StakeHolderService.GetAll();
        if (result.Succeeded)
        {
            StakeHolderResponseList = result.Data;
        }
    }

}
