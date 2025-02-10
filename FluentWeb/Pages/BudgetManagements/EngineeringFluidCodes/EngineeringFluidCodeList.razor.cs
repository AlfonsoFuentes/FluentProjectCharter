using Microsoft.AspNetCore.Components;
using Shared.Enums.ExportFiles;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.EngineeringFluidCodes.Records;
using Shared.Models.EngineeringFluidCodes.Requests;
using Shared.Models.EngineeringFluidCodes.Responses;
using Web.Infrastructure.Managers.Generic;
#nullable disable
namespace FluentWeb.Pages.BudgetManagements.EngineeringFluidCodes;
public partial class EngineeringFluidCodeList
{
    [CascadingParameter]
    public App App { get; set; }


    public List<EngineeringFluidCodeResponse> Items { get; set; } = new();
    string nameFilter { get; set; } = string.Empty;
    Func<EngineeringFluidCodeResponse, bool> fiterexpresion => x =>
       x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase);
    public List<EngineeringFluidCodeResponse> FilteredItems => Items.Count == 0 ? new() :
        Items.Where(fiterexpresion).ToList();

    [Inject]
    private IGenericService Service { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await UpdateAll();
    }
    async Task UpdateAll()
    {

        var result = await GenericService.GetAll<EngineeringFluidCodeResponseList, EngineeringFluidCodeGetAll>(new EngineeringFluidCodeGetAll());
        if (result.Succeeded)
        {
            Items = result.Data.Items;
        }
    }

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateEngineeringFluidCode");
    }
    void Edit(EngineeringFluidCodeResponse response)
    {
        Navigation.NavigateTo($"/UpdateEngineeringFluidCode/{response.Id}");
    }
    
   
    public async Task Delete(EngineeringFluidCodeResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteEngineeringFluidCodeRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
            };
            var resultDelete = await GenericService.Delete(request);
            if (resultDelete.Succeeded)
            {
                await UpdateAll();
                _snackBar.ShowSuccess(resultDelete.Messages);


            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }
   
}
