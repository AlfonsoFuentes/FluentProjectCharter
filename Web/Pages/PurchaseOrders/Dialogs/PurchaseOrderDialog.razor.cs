using Shared.Models.BudgetItems.Responses;
using Shared.Models.PurchaseOrders.Requests;
using Shared.Models.Suppliers.Responses;

namespace Web.Pages.PurchaseOrders.Dialogs;
public partial class PurchaseOrderDialog
{
    [Inject]
    public IRate _CurrencyService { get; set; } = null!;
    [Parameter]
    public PurchaseOrderRequest Model { get; set; } = null!;
    public ConversionRate RateList { get; set; } = null!;
    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }
    protected override async Task OnParametersSetAsync()
    {
        RateList = await _CurrencyService.GetRates(DateTime.UtcNow);
        var USDCOP = RateList == null ? 4000 : Math.Round(RateList.COP, 2);
        var USDEUR = RateList == null ? 1 : Math.Round(RateList.EUR, 2);

        if (Model != null)
        {
            Model.USDCOP = USDCOP;
            Model.USDEUR = USDEUR;
        }

    }
    [Parameter]
    public EventCallback ValidateAsync { get; set; }
    [Parameter]
    public bool EditPurchaseOrderItems { get; set; } = true;
    void ChangeNamePO()
    {
        if (Model.SelectedPurchaseOrderItems.Count == 1)
            Model.PurchaseOrderItems[0].Name = Model.Name;

    }
    void ChangeBudgetItemName(string name)
    {
        if (Model.SelectedPurchaseOrderItems.Count == 1)
            Model.Name = name;
    }

    void ChangeSupplier()
    {
        if (Model.Supplier != null)
            Model.PurchaseOrderCurrency = Model.Supplier.SupplierCurrency;
    }
    async Task DeleteItem(PurchaseOrderItemRequest selected)
    {
        Model.RemoveItem(selected);

        await ValidateAsync.InvokeAsync();
        
    }
    void AddBudgetItem()
    {

        Model.AddItem(new());
        StateHasChanged();
    }
    [Parameter]
    public List<BudgetItemWithPurchaseOrdersResponse> NonSelectedBudgetItems { get; set; } = new();
    public async Task ChangedCurrencyDate(DateTime? currentdate)
    {
        if (!currentdate.HasValue) return;
        if(currentdate.Value.Date > DateTime.UtcNow.Date)
        {
            _snackBar.ShowError($"Currency date must be lower than today {DateTime.UtcNow.Date}");
            Model.CurrencyDate = DateTime.UtcNow.Date;
            return; 
        }

        var result = await _CurrencyService.GetRates(currentdate.Value);
        if (result != null)
        {
            Model.USDCOP = result.COP;
            Model.USDEUR = result.EUR;

        }


    }
    [Parameter]
    public List<SupplierResponse> Suppliers { get; set; } = new();
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public EventCallback AddSupplier { get; set; }

}
