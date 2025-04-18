using MudBlazor;
using Shared.Models.BudgetItems.Records;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.Projects.Reponses;
using Web.Pages.PurchaseOrders.Dialogs;

namespace Web.Pages.BudgetItems;
public partial class BudgetItemsWithPurchaseOrders
{
    [Parameter]
    public ProjectResponse Project { get; set; } = new();

    BudgetItemWithPurchaseOrderResponseList ResponseList = new();
    public List<BudgetItemWithPurchaseOrdersResponse> Items => ResponseList.Items;
    protected override async Task OnParametersSetAsync()
    {
        await GetAll();
    }
    async Task GetAll()
    {
        var result = await GenericService.GetAll<BudgetItemWithPurchaseOrderResponseList, BudgetItemWithPurchaseOrderGetAll>(new BudgetItemWithPurchaseOrderGetAll()
        {
            ProjectId = Project.Id
        });
        if (result.Succeeded)
        {
            ResponseList = result.Data;
        }
    }
    async Task AddPurchaseorder(BudgetItemWithPurchaseOrdersResponse response)
    {
        var parameters = new DialogParameters<CreatePurchaseOrderDialog>
        {
            { x => x.BudgetItemId, response.Id},

        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.ExtraLarge };

        var dialog = await DialogService.ShowAsync<CreatePurchaseOrderDialog>("Create Purchase Order", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }
    }
    async Task EditPurchaseorderCreated(BudgetItemWithPurchaseOrdersResponse response)
    {
        var parameters = new DialogParameters<CreatePurchaseOrderDialog>
        {
            { x => x.BudgetItemId, response.Id},

        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.ExtraLarge };

        var dialog = await DialogService.ShowAsync<CreatePurchaseOrderDialog>("Create Purchase Order", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }
    }
}
