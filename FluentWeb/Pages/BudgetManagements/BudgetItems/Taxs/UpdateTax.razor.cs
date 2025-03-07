using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.Taxs.Records;
using Shared.Models.BudgetItems.IndividualItems.Taxs.Requests;
using Shared.Models.BudgetItems.IndividualItems.Taxs.Responses;
using static Shared.StaticClasses.StaticClass;

#nullable disable

namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Taxs;
public partial class UpdateTax
{
    [CascadingParameter]
    public App App { get; set; }
    UpdateTaxRequest Model = new();
  
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await GetTaxItems();
        var result = await GenericService.GetById<TaxResponse, GetTaxByIdRequest>(
            new GetTaxByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = result.Data.ProjectId,

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
        var result = await GenericService.GetAll<TaxItemResponseList, GetBudgetItemsToApplyTaxRequest>(new GetBudgetItemsToApplyTaxRequest() { ProjectId = App.ProjectId });
        if (result.Succeeded)
        {
            taxItemResponseList = result.Data;

        }
    }
    TaxItemResponseList taxItemResponseList = new();
}
