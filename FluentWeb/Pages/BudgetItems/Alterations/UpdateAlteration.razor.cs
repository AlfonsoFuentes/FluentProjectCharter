using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Alterations.Records;
using Shared.Models.BudgetItems.Alterations.Requests;
using Shared.Models.BudgetItems.Alterations.Responses;

namespace FluentWeb.Pages.BudgetItems.Alterations;
public partial class UpdateAlteration
{
    UpdateAlterationRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<AlterationResponse, GetAlterationByIdRequest>(
            new GetAlterationByIdRequest() { Id = Id, ProjectId = ProjectId });

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
