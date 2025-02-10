using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Records;
using Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Requests;
using Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Responses;

namespace FluentWeb.Pages.BudgetManagements.BudgetItems.EngineeringDesigns;
public partial class UpdateEngineeringDesign
{
    UpdateEngineeringDesignRequest Model = new();
 
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<EngineeringDesignResponse, GetEngineeringDesignByIdRequest>(
            new GetEngineeringDesignByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = result.Data.ProjectId,
                Budget = result.Data.Budget,

            };
        }
    }
}
