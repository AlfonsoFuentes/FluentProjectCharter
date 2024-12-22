using Microsoft.AspNetCore.Components;
using Shared.Models.HighLevelRequirements.Records;
using Shared.Models.HighLevelRequirements.Responses;
using Shared.Models.StakeHolders.Records;
using Shared.Models.StakeHolders.Requests;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.StakeHolders;
#nullable disable

namespace FluentWeb.Pages.StakeHolderInsideProject;
public partial class UpdateStakeHolderInsideProject
{
    UpdateStakeHolderInsideProjectRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        Model.ProjectId = ProjectId;
        await UpdateStakeHolder();
        var result = await GenericService.GetById<StakeHolderInsideProjectResponse, GetStakeHolderInsideProjectByIdRequest>(
            new GetStakeHolderInsideProjectByIdRequest() { Id = Id, ProjectId = ProjectId });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.StakeHolder.Id,
                StakeHolder = result.Data.StakeHolder,
                Role = result.Data.Role,
                ProjectId = ProjectId,
            };
            stakeHolder = Model.StakeHolder.Name;
        }
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
    void AddStakeHolder()
    {
        Navigation.NavigateTo($"/CreateStakeHolder");
    }
}
