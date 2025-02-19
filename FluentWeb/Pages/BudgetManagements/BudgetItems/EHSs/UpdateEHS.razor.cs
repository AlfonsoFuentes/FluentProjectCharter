using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Records;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Requests;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Responses;

namespace FluentWeb.Pages.BudgetManagements.BudgetItems.EHSs;
public partial class UpdateEHS
{
    UpdateEHSRequest Model = new();
    
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<EHSResponse, GetEHSByIdRequest>(
            new GetEHSByIdRequest() { Id = Id });

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
