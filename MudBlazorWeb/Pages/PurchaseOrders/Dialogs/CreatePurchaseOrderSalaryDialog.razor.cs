using Blazored.FluentValidation;
using MudBlazor;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.PurchaseOrders.Requests;
using Shared.Models.PurchaseOrders.Responses;

namespace MudBlazorWeb.Pages.PurchaseOrders.Dialogs;
public partial class CreatePurchaseOrderSalaryDialog
{
    [Inject]
    public IRate _CurrencyService { get; set; } = null!;
    public ConversionRate RateList { get; set; } = null!;
    FluentValidationValidator _fluentValidationValidator = null!;
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = null!;
    private bool Validated { get; set; } = false;
    async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }
    [Parameter]
    public BudgetItemWithPurchaseOrdersResponse BudgetItem { get; set; } = null!;
    CreateSalaryPurchaseOrderRequest Model { get; set; } = new();
    [Parameter]
    public BudgetItemWithPurchaseOrderResponseList ResponseList { get; set; } = new();
    public List<BudgetItemWithPurchaseOrdersResponse> OriginalBudgetItems => BudgetItem.IsAlteration ? ResponseList.Expenses : ResponseList.Capital;
   protected override async Task OnInitializedAsync()
    {
        RateList = await _CurrencyService.GetRates(DateTime.UtcNow);
        var USDCOP = RateList == null ? 4000 : Math.Round(RateList.COP, 2);
        var USDEUR = RateList == null ? 1 : Math.Round(RateList.EUR, 2);
       
        Model.IsProductiveAsset = ResponseList.IsProductiveAsset;
        Model.CostCenter = ResponseList.CostCenter;
        Model.ProjectId = ResponseList.ProjectId;
        Model.ProjectAccount = ResponseList.ProjectNumber;
        Model.CurrencyDate = DateTime.UtcNow;
        Model.IsAlteration = BudgetItem.IsAlteration;
        Model.IsTaxEditable = BudgetItem.IsTaxes;
        PurchaseOrderItemResponse item = new();
       
        if (OriginalBudgetItems.Any(x => x.Id == BudgetItem.Id))
        {
            item.BudgetItem = OriginalBudgetItems.Single(x => x.Id == BudgetItem.Id);
            Model.AddItem(item);
          
            Model.MainBudgetItemId = BudgetItem.Id;
            item.Quantity = 1;
        }
        Model.USDCOP = USDCOP;
        Model.USDEUR = USDEUR;



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
   
}
