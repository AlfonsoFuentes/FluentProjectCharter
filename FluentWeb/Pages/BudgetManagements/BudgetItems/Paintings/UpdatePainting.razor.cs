using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.Paintings.Records;
using Shared.Models.BudgetItems.IndividualItems.Paintings.Requests;
using Shared.Models.BudgetItems.IndividualItems.Paintings.Responses;

namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Paintings;
public partial class UpdatePainting
{
    UpdatePaintingRequest Model = new();

    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<PaintingResponse, GetPaintingByIdRequest>(
            new GetPaintingByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = result.Data.ProjectId,
                Quantity = result.Data.Quantity,
                UnitaryCost = result.Data.UnitaryCost,

            };
        }
    }
}
