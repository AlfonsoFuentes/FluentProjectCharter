using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Pipings.Records;
using Shared.Models.BudgetItems.Pipings.Requests;
using Shared.Models.BudgetItems.Pipings.Responses;

namespace FluentWeb.Pages.BudgetItems.Pipings;
public partial class UpdatePiping
{
    UpdatePipingRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<PipingResponse, GetPipingByIdRequest>(
            new GetPipingByIdRequest() { Id = Id, ProjectId = ProjectId });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = ProjectId,
                Diameter = result.Data.Diameter,
                Material = result.Data.Material,
                Insulation = result.Data.Insulation,
                FluidCodeName = result.Data.FluidCodeName,
                MaterialUnitaryCost = result.Data.MaterialUnitaryCost,
                MaterialQuantity = result.Data.MaterialQuantity,
                LaborUnitaryCost = result.Data.LaborUnitaryCost,
                LaborQuantity = result.Data.LaborQuantity,
                TagNumber = result.Data.TagNumber,



            };
        }
    }
}
