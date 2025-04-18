using Shared.Enums.CurrencyEnums;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.PurchaseOrders.Requests;

namespace Web.Pages.PurchaseOrders.Tables;
public partial class PurchaseOrderItemTable
{
    [Parameter]
    public bool EditPurchaseOrderItems { get; set; } = true;
    [Parameter, EditorRequired]
    public Guid MainBudgetItemId { get; set; }
    [Parameter, EditorRequired]
    public List<BudgetItemWithPurchaseOrdersResponse> NonSelectedBudgetItems { get; set; } = new();
    [Parameter]
    public List<PurchaseOrderItemRequest> Items { get; set; } = new();
    public List<PurchaseOrderItemRequest> OrderedItems => Items.Count==0 ? new() : Items.OrderBy(x => x.Order).ToList();
    [Parameter]
    public EventCallback<List<PurchaseOrderItemRequest>> ItemsChanged { get; set; }
    public bool IsSameCurrency => QuoteCurrency.Id == PurchaseOrderCurrency.Id;
    public bool QuoteIsUSD => QuoteCurrency.Id == CurrencyEnum.USD.Id;
    [Parameter, EditorRequired]
    public CurrencyEnum QuoteCurrency { get; set; } = CurrencyEnum.None;
    [Parameter, EditorRequired]
    public CurrencyEnum PurchaseOrderCurrency { get; set; } = CurrencyEnum.None;
    async Task OnItemsChanged()
    {
        if (ItemsChanged.HasDelegate)
        {
            await ItemsChanged.InvokeAsync(Items);
        }
    }
    [Parameter]
    public EventCallback ValidateAsync { get; set; }
    [Parameter]
    public EventCallback SelectItemChanged { get; set; }

    [Parameter]
    public EventCallback<PurchaseOrderItemRequest> DeleteItem { get; set; }
    async Task Delete(PurchaseOrderItemRequest selected)
    {

        await DeleteItem.InvokeAsync(selected);
      

    }
    [Parameter]
    public EventCallback<string> ChangeName { get; set; }
    async Task OnChangeName(string name)
    {
        if (ChangeName.HasDelegate)
        {
            await ChangeName.InvokeAsync(name);
        }
    }

}
