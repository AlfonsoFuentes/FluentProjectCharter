using Microsoft.AspNetCore.Components;
using Shared.Enums.CurrencyEnums;
using Shared.Models.BudgetItems.Records;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.PurchaseOrders.Requests;
using Shared.Models.Suppliers.Records;
using Shared.Models.Suppliers.Responses;
using Shared.StaticClasses;
using Web.Infrastructure.Services.Currencies;

namespace FluentWeb.Pages.PurchaseOrders;
public partial class CreatePurchaseOrder
{
    [Inject]
    public IRate _CurrencyService { get; set; } = null!;
    [CascadingParameter]
    public App MainApp { get; set; } = null!;
    [Parameter]
    public Guid BudgetItemId { get; set; }

    List<BudgetItemWithPurchaseOrdersResponse> OriginalBudgetItems = new();
    List<BudgetItemWithPurchaseOrdersResponse> NonSelectedBudgetItems => OriginalBudgetItems.Where(x => x.Id != Model.MainBudgetItemId).ToList();
    List<SupplierResponse> Suppliers { get; set; } = new();
    public CreatePurchaseOrderRequest Model = new();
    protected override async Task OnInitializedAsync()
    {
        await LoadFromLocalStorage();

        await GetSuppliers();
        if (Model == null)
        {
            Model = new();
            var USDCOP = MainApp.RateList == null ? 4000 : Math.Round(MainApp.RateList.COP, 2);
            var USDEUR = MainApp.RateList == null ? 1 : Math.Round(MainApp.RateList.EUR, 2);
            Model.USDCOP = USDCOP;
            Model.USDEUR = USDEUR;
            var resultMainBudgetItem = await GenericService.GetById<BudgetItemWithPurchaseOrdersResponse, BudgetItemWithPurchaseOrderGetById>(new BudgetItemWithPurchaseOrderGetById()
            {
                Id = BudgetItemId,
            });
            if (resultMainBudgetItem.Succeeded)
            {
                var resultProjectt = await GenericService.GetAll<BudgetItemWithPurchaseOrderResponseList, BudgetItemWithPurchaseOrderGetAll>(new BudgetItemWithPurchaseOrderGetAll()
                {
                    ProjectId = resultMainBudgetItem.Data.ProjectId,
                });
                if (resultProjectt.Succeeded)
                {
                    if (resultMainBudgetItem.Data.IsAlteration)
                    {
                        OriginalBudgetItems = resultProjectt.Data.Expenses;
                    }
                    else
                    {
                        OriginalBudgetItems = resultProjectt.Data.Capital;
                    }
                    Model.IsProductive = resultProjectt.Data.IsProductive;
                    Model.CostCenter = resultProjectt.Data.CostCenter;
                    Model.ProjectId = resultProjectt.Data.ProjectId;
                    Model.ProjectAccount = resultProjectt.Data.ProjectNumber;
                    Model.QuoteCurrency = CurrencyEnum.COP;
                    Model.PurchaseOrderCurrency = CurrencyEnum.COP;
                    PurchaseOrderItemRequest item = new();
                    if (OriginalBudgetItems.Any(x => x.Id == BudgetItemId))
                    {
                        item.BudgetItem = OriginalBudgetItems.First(x => x.Id == BudgetItemId) ;
                        Model.AddItem(item);

                        Model.MainBudgetItemId = BudgetItemId;

                    }


                }


            }
        }

    }
    async Task GetSuppliers()
    {
        var result = await GenericService.GetAll<SupplierResponseList, SupplierGetAll>(new SupplierGetAll());
        if (result.Succeeded)
        {
            Suppliers = result.Data.Items;
        }
    }
    void Edit(PurchaseOrderItemRequest row)
    {
        EditRow = row;
        CancelCreate();
    }
    void Delete(PurchaseOrderItemRequest row)
    {
        Model.PurchaseOrderItems.Remove(row);
        EditRow = null!;
        CreateRow = null!;
    }


    void AddSuplier()
    {
        SaveModelToLocalStorage().ContinueWith(_ =>
        {
            Navigation.NavigateTo(StaticClass.Suppliers.PageName.Create);
        });

    }
    private async Task SaveModelToLocalStorage()
    {
        await _localModelStorage.SaveToLocalStorage(Model);
    }
    async Task LoadFromLocalStorage()
    {
        Model = await _localModelStorage.LoadFromLocalStorage(Model) ?? null!;
    }
    public async Task ChangedCurrencyDate(DateTime? currentdate)
    {
        if (!currentdate.HasValue) return;

        var result = await _CurrencyService.GetRates(currentdate.Value);
        if (result != null)
        {
            Model.USDCOP = result.COP;
            Model.USDEUR = result.EUR;

        }


    }
    bool IsSameCurrency => Model == null ? false : Model.QuoteCurrency.Id == Model.PurchaseOrderCurrency.Id;
    PurchaseOrderItemRequest CreateRow = null!;
    PurchaseOrderItemRequest EditRow = null!;

    void AddNew()
    {
        CreateRow = new();
        Model.AddItem(CreateRow);
    }
    void CancelCreate()
    {
        if (CreateRow == null) return;


        CreateRow = null!;
    }
    void CancelEdit()
    {
        if (EditRow == null) return;
        EditRow = null!;
    }
    void SetSupplier(SupplierResponse supplier)
    {
        Model.PurchaseOrderCurrency = supplier.SupplierCurrency;
    }

}
