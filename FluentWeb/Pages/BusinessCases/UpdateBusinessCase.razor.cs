using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Records;
using Shared.Models.Cases.Requests;
using Shared.Models.Cases.Responses;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.OrganizationStrategies.Responses;
using Web.Infrastructure.Managers.OrganizationStrategys;
#nullable disable
namespace FluentWeb.Pages.BusinessCases;
public partial class UpdateBusinessCase
{
    UpdateCaseRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }

    private void CancelAsync()
    {
        Navigation.NavigateBack();

    }
    [Inject]
    private IOrganizationStrategyService ServiceOrganizationStrategy { get; set; }

    OrganizationStrategyResponseList OrganizationResponse = new();
    protected override async Task OnInitializedAsync()
    {
        await UpdateOrganizationStrategy();
        var result = await GenericService.GetById<CaseResponse, GetCaseByIdRequest>(new GetCaseByIdRequest() { Id = Id });
        Model = new()
        {
            Id = result.Data.Id,
            Name = result.Data.Name,
            ProjectId = ProjectId,
            OrganizationStrategy = result.Data.OrganizationStrategy,

        };
        SelectedValue = Model.OrganizationStrategy == null ? string.Empty : Model.OrganizationStrategy.Name;
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
