using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Commons;
using Shared.Enums.CurrencyEnums;
using Shared.Models.AcceptanceCriterias.Responses;
using Shared.Models.BudgetItems.Records;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.FileResults.Generics.Reponses;
using Shared.Models.PurchaseOrders.Requests;
using Shared.Models.Suppliers.Records;
using Shared.Models.Suppliers.Responses;
using Shared.StaticClasses;
using System.Diagnostics.Metrics;
using Web.Infrastructure.Services.Currencies;
using static Shared.StaticClasses.StaticClass;

namespace FluentWeb.Pages.PurchaseOrders;
public partial class CreatePurchaseOrder
{
    [Inject]
    public IRate _CurrencyService { get; set; } = null!;
    [CascadingParameter]
    public App MainApp { get; set; } = null!;
    [Parameter]
    public Guid BudgetItemId { get; set; }

    List<IBudgetItemResponse> BudgetItems = new();
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
                Model.AddPurchaseorderItem(resultMainBudgetItem.Data);
                var resultProjectt = await GenericService.GetAll<BudgetItemResponseList, BudgetItemGetAll>(new BudgetItemGetAll()
                {
                    ProjectId = resultMainBudgetItem.Data.ProjectId,
                });
                if (resultProjectt.Succeeded)
                {
                    if (resultMainBudgetItem.Data.IsAlteration)
                    {
                        BudgetItems = resultProjectt.Data.Expenses;
                    }
                    else
                    {
                        BudgetItems = resultProjectt.Data.Capital;
                    }
                    Model.IsProductive = resultProjectt.Data.IsProductive;
                    Model.CostCenter = resultProjectt.Data.CostCenter;
                    Model.ProjectAccount = resultProjectt.Data.ProjectNumber;
                }


            }
        }
        


        //Consultar Budget Item para sacar datos del BudgetItem Principal
        //Consultar Project para sacar datos
        //Sacar el listado de posibles budgetitems
    }
    async Task GetSuppliers()
    {
        var result = await GenericService.GetAll<SupplierResponseList, SupplierGetAll>(new SupplierGetAll());
        if (result.Succeeded)
        {
            Suppliers = result.Data.Items;
        }
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
    CreatePurchaseOrderItemRequest CreateRow = null!;
    CreatePurchaseOrderItemRequest EditRow = null!;
    CreatePurchaseOrderItemRequest SelectedRow = null!;
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
    private void HandleRowClick(FluentDataGridRow<CreatePurchaseOrderItemRequest> row)
    {
        SelectedRow = row.Item == null ? null! : SelectedRow == null! ? row.Item : null!;
        //Si EditRow es diferente al seleccionado se vuelve null para desaparecer la caja de texto
        EditRow = EditRow == null ? null! : SelectedRow == null ? null! :  EditRow;



    }
    private void HandleRowDoubleClick(FluentDataGridRow<CreatePurchaseOrderItemRequest> row)
    {
        EditRow = EditRow == null ? null! : SelectedRow == null ? null! : EditRow;
        //Si CreateRow esta creada se elimina del listado y se vueleve null
        CancelCreate();

    }

}
