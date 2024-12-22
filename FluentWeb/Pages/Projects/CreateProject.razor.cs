using Microsoft.AspNetCore.Components;
using Shared.Enums.StakeHolderTypes;
using Shared.Models.Projects.Request;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.StakeHolders;
#nullable disable
namespace FluentWeb.Pages.Projects;
public partial class CreateProject
{
    CreateProjectRequest Model = new();

    protected override async Task OnInitializedAsync()
    {
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
    void AddStakeHolder(StakeHolderRoleEnum Role)
    {
        Navigation.NavigateTo($"/CreateStakeHolder");
    }
}
