using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Foundations.Records;
using Shared.Models.BudgetItems.Foundations.Requests;
using Shared.Models.BudgetItems.Foundations.Responses;

namespace FluentWeb.Pages.BudgetItems.Foundations;
public partial class UpdateFoundation
{
    UpdateFoundationRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<FoundationResponse, GetFoundationByIdRequest>(
            new GetFoundationByIdRequest() { Id = Id, ProjectId = ProjectId });

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
