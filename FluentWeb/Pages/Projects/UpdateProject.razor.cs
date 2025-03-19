using Microsoft.AspNetCore.Components;
using Shared.Enums.StakeHolderTypes;
using Shared.Models.Projects.Mappers;
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
            Model = result.Data.ToUpdate();
    
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
