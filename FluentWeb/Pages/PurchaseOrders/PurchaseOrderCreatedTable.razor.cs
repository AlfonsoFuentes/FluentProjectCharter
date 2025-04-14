
using Shared.Models.Projects.Reponses;
using Shared.Models.Projects.Request;
using Shared.Models.PurchaseOrders.Records;
using Shared.Models.PurchaseOrders.Requests;
using Shared.Models.PurchaseOrders.Responses;

namespace FluentWeb.Pages.PurchaseOrders;
public partial class PurchaseOrderCreatedTable
{

    //protected override async Task OnInitializedAsync()
    //{
    //    await GetAll();
    //}
    //PurchaseOrderResponseList Response { get; set; } = new();
    //List<PurchaseOrderResponse> Items => Response.Items;
    //async Task GetAll()
    //{
    //    var result = await GenericService.GetAll<PurchaseOrderResponseList, PurchaseOrderCreatedGetAll>(new PurchaseOrderCreatedGetAll());
    //    if (result.Succeeded)
    //    {
    //        Response = result.Data;
    //    }
    //}
    //string nameFilter { get; set; } = string.Empty;
    //Func<PurchaseOrderResponse, bool> fiterexpresion => x =>
    //   x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase);
    //public List<PurchaseOrderResponse> FilteredItems => Items.Count == 0 ? new() : Items.Where(fiterexpresion).ToList();

    //void Edit(PurchaseOrderResponse response)
    //{

    //}
    ////async Task Delete(PurchaseOrderResponse response)
    ////{
    ////    //var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
    ////    //var result = await dialog.Result;
    ////    //var canceled = result.Cancelled;



    ////    //if (!canceled)
    ////    //{
    ////    //    DeletePurchaseOrderRequest request = new()
    ////    //    {
    ////    //        Id = response.Id,
    ////    //        Name = response.Name,
    ////    //    };
    ////    //    var resultDelete = await GenericService.Delete(request);
    ////    //    if (resultDelete.Succeeded)
    ////    //    {
    ////    //        await GetAll();
    ////    //        _snackBar.ShowSuccess(resultDelete.Messages);


    ////    //    }
    ////    //    else
    ////    //    {
    ////    //        _snackBar.ShowError(resultDelete.Messages);
    ////    //    }
    ////    //}
    ////}
    ////void Approve(PurchaseOrderResponse response)
    ////{

    //}
}
