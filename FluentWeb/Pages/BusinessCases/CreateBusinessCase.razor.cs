using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Requests;
using Shared.Models.IdentityModels.Responses.Audit;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.OrganizationStrategies.Responses;
using Web.Infrastructure.Managers.OrganizationStrategys;
#nullable disable
namespace FluentWeb.Pages.BusinessCases;
public partial class CreateBusinessCase
{
    CreateCaseRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }

    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;
 
    }
    private void CancelAsync()
    {
        Navigation.NavigateBack();

    }
    [Inject]
    private IOrganizationStrategyService ServiceOrganizationStrategy { get; set; }

    OrganizationStrategyResponseList OrganizationResponse = new();
    protected override async Task OnParametersSetAsync()
    {
        await UpdateOrganizationStrategy();
    }
    async Task UpdateOrganizationStrategy()
    {
        var result = await ServiceOrganizationStrategy.GetAll();
        if (result.Succeeded)
        {
            OrganizationResponse = result.Data;
        }
    }

    public void AddOrganizationStrategy()
    {
        Navigation.NavigateTo($"/CreateOrganizationStrategy");
    }
   
}
