using Microsoft.AspNetCore.Components;
using Shared.Enums.StakeHolderTypes;
using Shared.Models.StakeHolders.Requests;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.StakeHolders;
#nullable disable
namespace FluentWeb.Pages.StakeHolderInsideProject;
public partial class CreateStakeHolderInsideProject
{
    CreateStakeHolderInsideProjectRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    protected override async Task OnInitializedAsync()
    {
        Model.ProjectId = ProjectId;
        await UpdateStakeHolder();
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
