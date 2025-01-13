using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Structurals.Records;
using Shared.Models.BudgetItems.Structurals.Requests;
using Shared.Models.BudgetItems.Structurals.Responses;

namespace FluentWeb.Pages.BudgetItems.Structurals;
public partial class UpdateStructural
{
    UpdateStructuralRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<StructuralResponse, GetStructuralByIdRequest>(
            new GetStructuralByIdRequest() { Id = Id, ProjectId = ProjectId });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = ProjectId,
                Quantity = result.Data.Quantity,
                UnitaryCost = result.Data.UnitaryCost,

            };
        }
    }
}
