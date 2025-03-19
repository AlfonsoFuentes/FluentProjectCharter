using Microsoft.AspNetCore.Components;
using Shared.Models.Projects.Mappers;
using Shared.Models.Projects.Records;
using Shared.Models.Projects.Reponses;
using Shared.Models.Projects.Request;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.StakeHolders;

namespace FluentWeb.Pages.Projects;
public partial class ApproveProject
{
    ApproveProjectRequest Model = new();
    [Parameter]
    public Guid Id { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await UpdateStakeHolder();
        var result = await GenericService.GetById<ProjectResponse, GetProjectByIdRequest>(new GetProjectByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = result.Data.ToApprove();
   
        }
    }
    [Inject]
    private IStakeHolderService StakeHolderService { get; set; } = null!;
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
