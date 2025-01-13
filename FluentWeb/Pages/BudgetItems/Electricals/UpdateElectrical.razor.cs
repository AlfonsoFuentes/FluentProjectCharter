using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Electricals.Records;
using Shared.Models.BudgetItems.Electricals.Requests;
using Shared.Models.BudgetItems.Electricals.Responses;

namespace FluentWeb.Pages.BudgetItems.Electricals;
public partial class UpdateElectrical
{
    UpdateElectricalRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<ElectricalResponse, GetElectricalByIdRequest>(
            new GetElectricalByIdRequest() { Id = Id, ProjectId = ProjectId });

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
