using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Taxs.Records;
using Shared.Models.BudgetItems.Taxs.Requests;
using Shared.Models.BudgetItems.Taxs.Responses;

namespace FluentWeb.Pages.BudgetItems.Taxs;
public partial class CreateTax
{
    CreateTaxRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid DeliverableId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetTaxItems();
      
        Model.ProjectId = ProjectId;
        Model.DeliverableId = DeliverableId;
    }
    TaxItemResponseList taxItemResponseList = new();

    async Task GetTaxItems()
    {
        var result = await GenericService.GetAll<TaxItemResponseList, GetBudgetItemsToApplyTaxRequest>(new GetBudgetItemsToApplyTaxRequest() { ProjectId = ProjectId });
        if (result.Succeeded)
        {
            taxItemResponseList = result.Data;

        }
    }

}
