using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Taxs.Records;
using Shared.Models.BudgetItems.Taxs.Requests;
using Shared.Models.BudgetItems.Taxs.Responses;
using static Shared.StaticClasses.StaticClass;

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
        await GetTaxItems();
        var result = await GenericService.GetById<TaxResponse, GetTaxByIdRequest>(
            new GetTaxByIdRequest() { Id = Id, ProjectId = ProjectId });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = ProjectId,
               
                TaxItems = result.Data.TaxItems,
                Percentage = result.Data.Percentage,

            };
            Model.TaxItems.ForEach(x =>
            {
                var item = taxItemResponseList.Items.FirstOrDefault(y => y.BudgetItemId == x.BudgetItemId);
                if (item != null)
                {
                    item.Selected = true;
                }

            });
        }


    }
    async Task GetTaxItems()
    {
        var result = await GenericService.GetAll<TaxItemResponseList, GetBudgetItemsToApplyTaxRequest>(new GetBudgetItemsToApplyTaxRequest() { ProjectId = ProjectId });
        if (result.Succeeded)
        {
            taxItemResponseList = result.Data;

        }
    }
    TaxItemResponseList taxItemResponseList = new();
}
