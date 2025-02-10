using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.Electricals.Records;
using Shared.Models.BudgetItems.IndividualItems.Electricals.Requests;
using Shared.Models.BudgetItems.IndividualItems.Electricals.Responses;

namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Electricals;
public partial class UpdateElectrical
{
    UpdateElectricalRequest Model = new();

    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<ElectricalResponse, GetElectricalByIdRequest>(
            new GetElectricalByIdRequest() { Id = Id });

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
