using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Records;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Requests;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Responses;

namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Foundations;
public partial class UpdateFoundation
{
    UpdateFoundationRequest Model = new();

    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<FoundationResponse, GetFoundationByIdRequest>(
            new GetFoundationByIdRequest() { Id = Id });

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
