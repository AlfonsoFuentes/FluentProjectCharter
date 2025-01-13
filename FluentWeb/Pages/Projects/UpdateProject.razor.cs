using Microsoft.AspNetCore.Components;
using Shared.Enums.StakeHolderTypes;
using Shared.Models.Projects.Records;
using Shared.Models.Projects.Reponses;
using Shared.Models.Projects.Request;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.StakeHolders;
#nullable disable
namespace FluentWeb.Pages.Projects;
public partial class UpdateProject
{
    UpdateProjectRequest Model = new();
    [Parameter]
    public Guid Id { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await UpdateStakeHolder();
        var result = await GenericService.GetById<ProjectResponse, GetProjectByIdRequest>(new GetProjectByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectNeedType = result.Data.ProjectNeedType,
                InitialBudget = result.Data.InitialBudget,
                ProjectDescription = result.Data.ProjectDescription,
                Manager = result.Data.Manager,
                Sponsor = result.Data.Sponsor,
                InitialProjectDate = result.Data.InitialProjectDate,
                Status = result.Data.Status,
                PercentageContingency = result.Data.PercentageContingency,
                PercentageEngineering = result.Data.PercentageEngineering,
                PercentageTaxProductive=result.Data.PercentageTaxes,
                IsProductiveAsset = result.Data.IsProductive

            };
            manager = Model.Manager == null ? string.Empty : Model.Manager.Name;
            sponsor = Model.Sponsor == null ? string.Empty : Model.Sponsor.Name;
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
