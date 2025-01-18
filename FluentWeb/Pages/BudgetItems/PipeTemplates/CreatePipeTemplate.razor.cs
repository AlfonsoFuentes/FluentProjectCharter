using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.Pipes.Requests;
using Shared.Models.Templates.Pipes.Requests;
using Shared.Models.Temporarys.Requests;
using Shared.StaticClasses;

namespace FluentWeb.Pages.BudgetItems.PipeTemplates;
public partial class CreatePipeTemplate
{
    CreatePipeTemplateRequest Model = new();

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
       Navigation.NavigateTo(StaticClass.Brands.PageName.Create);
    }
}
