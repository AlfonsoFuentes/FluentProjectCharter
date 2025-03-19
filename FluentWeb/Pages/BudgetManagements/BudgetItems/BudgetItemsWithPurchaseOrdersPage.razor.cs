using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Records;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.FileResults.Generics.Reponses;

namespace FluentWeb.Pages.BudgetManagements.BudgetItems;
public partial class BudgetItemsWithPurchaseOrdersPage
{
    [Parameter]
    public Guid ProjectId { get; set; }

    BudgetItemWithPurchaseOrderResponseList Response = new();
    async Task GetAll()
    {
        var result = await GenericService.GetAll<BudgetItemWithPurchaseOrderResponseList, BudgetItemWithPurchaseOrderGetAll>(new BudgetItemWithPurchaseOrderGetAll()
        {
            ProjectId = ProjectId
        });
        if (result.Succeeded)
        {
            Response = result.Data;
        }
    }
    protected override async Task OnInitializedAsync()
    {
        await GetAll();
    }
    string nameFilter=string.Empty;
    public List<IBudgetItemWithPurchaseOrderResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Response.Items : Response.Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    void AddPurchaseOrder(IBudgetItemWithPurchaseOrderResponse budgetItem)
    {
        Navigation.NavigateTo($"/CreatePurchaseOrder/{budgetItem.Id}");
    }
}
