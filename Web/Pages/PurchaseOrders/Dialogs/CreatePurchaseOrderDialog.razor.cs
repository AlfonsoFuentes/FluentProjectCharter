using Blazored.FluentValidation;
using MudBlazor;
using Shared.Enums.CurrencyEnums;
using Shared.Models.BudgetItems.Records;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.PurchaseOrders.Requests;
using Shared.Models.Suppliers.Records;
using Shared.Models.Suppliers.Responses;
using Web.Pages.Suppliers;

namespace Web.Pages.PurchaseOrders.Dialogs;
public partial class CreatePurchaseOrderDialog
{
    FluentValidationValidator _fluentValidationValidator = null!;
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = null!;
    private bool Validated { get; set; } = false;
    async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }
    [Parameter]
    public Guid BudgetItemId { get; set; }
    CreatePurchaseOrderRequest Model { get; set; } = new();

    List<BudgetItemWithPurchaseOrdersResponse> OriginalBudgetItems = new();
    List<BudgetItemWithPurchaseOrdersResponse> NonSelectedBudgetItems = new();
    public List<SupplierResponse> Suppliers { get; set; } = new();
    async Task GetSuppliers()
    {
        var result = await GenericService.GetAll<SupplierResponseList, SupplierGetAll>(new SupplierGetAll());
        if (result.Succeeded)
        {
            Suppliers = result.Data.Items;
        }
    }
    protected override async Task OnInitializedAsync()
    {
        await GetSuppliers();

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
                NonSelectedBudgetItems = OriginalBudgetItems;
                Model.IsProductive = resultProjectt.Data.IsProductive;
                Model.CostCenter = resultProjectt.Data.CostCenter;
                Model.ProjectId = resultProjectt.Data.ProjectId;
                Model.ProjectAccount = resultProjectt.Data.ProjectNumber;
                Model.QuoteCurrency = CurrencyEnum.COP;

                PurchaseOrderItemRequest item = new();
                if (OriginalBudgetItems.Any(x => x.Id == BudgetItemId))
                {
                    item.BudgetItem = OriginalBudgetItems.Single(x => x.Id == BudgetItemId);
                    Model.AddItem(item);
                    NonSelectedBudgetItems.Remove(item.BudgetItem!);
                    Model.MainBudgetItemId = BudgetItemId;

                }
                Model.PurchaseOrderCurrency = CurrencyEnum.COP;
                Model.AddItem(new());
            }



        }
    }
   


    private async Task Submit()
    {
        var result = await GenericService.Post(Model);


        if (result.Succeeded)
        {
            _snackBar.ShowSuccess(result.Messages);
            MudDialog.Close(DialogResult.Ok(true));
        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }

    }


    private void Cancel() => MudDialog.Cancel();
    public async Task AddSupplier()
    {

        var parameters = new DialogParameters<SupplierDialog>
        {

        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<SupplierDialog>("Supplier", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetSuppliers();

        }
    }

}
