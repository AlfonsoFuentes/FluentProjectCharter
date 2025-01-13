using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.Valves.Requests;
using Shared.Models.Templates.Valves.Requests;
using Shared.Models.Temporarys.Requests;
using Shared.StaticClasses;

namespace FluentWeb.Pages.BudgetItems.ValveTemplates;
public partial class CreateValveTemplate
{
    CreateValveTemplateRequest Model = new();

    BrandResponseList BrandsResponseList { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        await GetBrands();

       
    }
    async Task GetBrands()
    {
        var result = await GenericService.GetAll<BrandResponseList, BrandGetAll>(new BrandGetAll());
        if (result.Succeeded)
        {
            BrandsResponseList = result.Data;
        }
    }
    void AddBrand()
    {
        //CreateTemporaryRequest temporaryRequest = new()
        //{
        //    Model = Model.Model,
           

        //};
        //var result = await GenericService.Create(temporaryRequest);

        Navigation.NavigateTo(StaticClass.Brands.PageName.Create);
    }
}
