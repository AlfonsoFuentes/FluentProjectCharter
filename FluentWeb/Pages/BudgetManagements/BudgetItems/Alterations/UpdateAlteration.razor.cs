using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Records;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Requests;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Responses;

namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Alterations;
public partial class UpdateAlteration
{
    UpdateAlterationRequest Model = new();
   
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<AlterationResponse, GetAlterationByIdRequest>(
            new GetAlterationByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
         
                Quantity = result.Data.Quantity,
                UnitaryCost = result.Data.UnitaryCost,
                ProjectId = result.Data.ProjectId,

            };
        }
    }
}
