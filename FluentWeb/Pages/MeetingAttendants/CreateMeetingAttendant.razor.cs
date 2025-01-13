using Microsoft.AspNetCore.Components;
using Shared.Models.MeetingAttendants.Requests;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.StakeHolders;
#nullable disable
namespace FluentWeb.Pages.MeetingAttendants;
public partial class CreateMeetingAttendant
{
    CreateMeetingAttendantRequest Model = new();
    [Parameter]
    public Guid MeetingId { get; set; }
    [Parameter]
    public Guid ProjectId { get; set; }
 
    protected override async Task OnInitializedAsync()
    {
        Model.MeetingId = MeetingId;
        Model.ProjectId = ProjectId;
        await UpdateStakeHolder();
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
