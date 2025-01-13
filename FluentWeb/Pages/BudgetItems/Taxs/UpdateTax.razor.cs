using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Taxs.Records;
using Shared.Models.BudgetItems.Taxs.Requests;
using Shared.Models.BudgetItems.Taxs.Responses;

namespace FluentWeb.Pages.BudgetItems.Taxs;
public partial class UpdateTax
{
    UpdateTaxRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<TaxResponse, GetTaxByIdRequest>(
            new GetTaxByIdRequest() { Id = Id, ProjectId = ProjectId });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = ProjectId,
                Budget = result.Data.Budget,

            };
        }
    }
}
