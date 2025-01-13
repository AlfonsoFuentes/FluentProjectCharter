using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Paintings.Records;
using Shared.Models.BudgetItems.Paintings.Requests;
using Shared.Models.BudgetItems.Paintings.Responses;

namespace FluentWeb.Pages.BudgetItems.Paintings;
public partial class UpdatePainting
{
    UpdatePaintingRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<PaintingResponse, GetPaintingByIdRequest>(
            new GetPaintingByIdRequest() { Id = Id, ProjectId = ProjectId });

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
