using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.EngineeringDesigns.Records;
using Shared.Models.BudgetItems.EngineeringDesigns.Requests;
using Shared.Models.BudgetItems.EngineeringDesigns.Responses;

namespace FluentWeb.Pages.BudgetItems.EngineeringDesigns;
public partial class UpdateEngineeringDesign
{
    UpdateEngineeringDesignRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<EngineeringDesignResponse, GetEngineeringDesignByIdRequest>(
            new GetEngineeringDesignByIdRequest() { Id = Id, ProjectId = ProjectId });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = ProjectId,
                Budget = result.Data.Budget,

            };
        }
    }
}
