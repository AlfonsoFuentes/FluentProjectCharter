using Microsoft.AspNetCore.Components;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Suppliers.Records;
using Shared.Models.Suppliers.Requests;
using Shared.Models.Suppliers.Responses;
using Shared.Models.Deliverables.Responses;
using Shared.StaticClasses;

namespace FluentWeb.Pages.Suppliers;
#nullable disable
public partial class SupplierList
{
    [CascadingParameter]
    public App App { get; set; }


   public List<SupplierResponse> Items { get; set; } = new();
    string nameFilter;
    public List<SupplierResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    protected override async Task OnInitializedAsync()
    {
        await GetAll();
    }
    async Task GetAll()
    {
        var result = await GenericService.GetAll<SupplierResponseList, SupplierGetAll>(new SupplierGetAll());
        if (result.Succeeded)
        {
            Items = result.Data.Items;
        }
    }
    public void AddNew()
    {
        Navigation.NavigateTo($"/{StaticClass.Suppliers.PageName.Create}");

    }



    void Edit(SupplierResponse response)
    {
        Navigation.NavigateTo($"/{StaticClass.Suppliers.PageName.Update}/{response.Id}");
    }
    public async Task Delete(SupplierResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteSupplierRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,

            };
            var resultDelete = await GenericService.Delete(request);
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
