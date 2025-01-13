using Microsoft.AspNetCore.Components;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Requests;
using Shared.Models.Brands.Responses;
using Shared.Models.Deliverables.Responses;
using Shared.StaticClasses;

namespace FluentWeb.Pages.Brands;
#nullable disable
public partial class BrandList
{
    [CascadingParameter]
    public App App { get; set; }


   public List<BrandResponse> Items { get; set; } = new();
    string nameFilter;
    public List<BrandResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    protected override async Task OnInitializedAsync()
    {
        await GetAll();
    }
    async Task GetAll()
    {
        var result = await GenericService.GetAll<BrandResponseList, BrandGetAll>(new BrandGetAll());
        if (result.Succeeded)
        {
            Items = result.Data.Items;
        }
    }
    public void AddNew()
    {
        Navigation.NavigateTo($"/{StaticClass.Brands.PageName.Create}");

    }



    void Edit(BrandResponse response)
    {
        Navigation.NavigateTo($"/{StaticClass.Brands.PageName.Update}/{response.Id}");
    }
    public async Task Delete(BrandResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteBrandRequest request = new()
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
