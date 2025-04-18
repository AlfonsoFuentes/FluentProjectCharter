
using MudBlazor;
using Shared.Enums.PurchaseOrderStatusEnums;
using Shared.Models.FileResults.Generics.Reponses;
using Shared.Models.PurchaseOrders.Records;
using Shared.Models.PurchaseOrders.Requests;
using Shared.Models.PurchaseOrders.Responses;
using Shared.Models.Suppliers.Requests;
using Shared.Models.Suppliers.Responses;
using Web.Pages.PurchaseOrders.Dialogs;
using Web.Templates;

namespace Web.Pages.PurchaseOrders.Tables;
public partial class PurchaseOrdersTable
{
    
    [Parameter]
    public PurchaseOrderStatusEnum Status { get; set; } = PurchaseOrderStatusEnum.None;
    string nameFilter = string.Empty;
    public Func<PurchaseOrderResponse, bool> Criteria => x => x.Name.Contains(nameFilter, StringComparison.InvariantCultureIgnoreCase);
    public List<PurchaseOrderResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items :
        Items.Where(Criteria).ToList();

    [Parameter]
    public List<PurchaseOrderResponse> Items { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        if (Items.Count == 0)
        {
            await GetAll();
        }


    }

    async Task GetAll()
    {

        var result = await GenericService.GetAll<PurchaseOrderResponseList, PurchaseOrderGetAll>(new PurchaseOrderGetAll()
        {
            Status = Status
        });
        if (result.Succeeded)
        {

            Items = result.Data.Items;
        }
    }
    async Task EditPurchaseOrderCreated(PurchaseOrderResponse purchaseOrder)
    {
        var parameters = new DialogParameters<EditPurchaseOrderCreatedDialog>
        {
            { x => x.Response, purchaseOrder},

        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.ExtraLarge };

        var dialog = await DialogService.ShowAsync<EditPurchaseOrderCreatedDialog>("Edit Purchase Order Created", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }
    }
    async Task ApprovePurchaseOrder(PurchaseOrderResponse purchaseOrder)
    {
        var parameters = new DialogParameters<ApprovePurchaseDialog>
        {
            { x => x.Response, purchaseOrder},

        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.ExtraLarge };

        var dialog = await DialogService.ShowAsync<ApprovePurchaseDialog>("Approve Purchase Order", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }
    }

    async Task EditPurchaseOrderApproved(PurchaseOrderResponse purchaseOrder)
    {
        var parameters = new DialogParameters<EditPurchaseOrderApprovedDialog>
        {
            { x => x.Response, purchaseOrder},

        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.ExtraLarge };

        var dialog = await DialogService.ShowAsync<EditPurchaseOrderApprovedDialog>("Approve Purchase Order", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }
    }
    async Task ReceivePurchaseOrder(PurchaseOrderResponse purchaseOrder)
    {
    }
    
    async Task EditPurchaseOrderClosed(PurchaseOrderResponse purchaseOrder)
    {
    }
   
    public async Task Delete(PurchaseOrderResponse response)
    {
        var parameters = new DialogParameters<DialogTemplate>
        {
            { x => x.ContentText, $"Do you really want to delete {response.Name}? This process cannot be undone." },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<DialogTemplate>("Delete", parameters, options);
        var result = await dialog.Result;


        if (!result!.Canceled)
        {
            DeletePurchaseOrderRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,

            };
            var resultDelete = await GenericService.Post(request);
            if (resultDelete.Succeeded)
            {
                await GetAll();
                _snackBar.ShowSuccess(resultDelete.Messages);


            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }
}
