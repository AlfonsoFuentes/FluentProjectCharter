
using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.Templates.Pipes.Records;
using Shared.Models.Templates.Pipes.Requests;
using Shared.Models.Templates.Pipes.Responses;
using Shared.StaticClasses;

namespace FluentWeb.Pages.BudgetItems.PipeTemplates;
public partial class UpdatePipeTemplate
{
    UpdatePipeTemplateRequest Model = new();
    [Parameter]
    public Guid Id { get; set; }
    BrandResponseList BrandsResponseList { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        await GetBrands();
        var result = await GenericService.GetById<PipeTemplateResponse, GetPipeTemplateByIdRequest>(
           new GetPipeTemplateByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,

                Material = result.Data.Material,

                BrandResponse = result.Data.BrandResponse,
                Diameter = result.Data.Diameter,
                Class = result.Data.Class,
                EquivalentLenghPrice = result.Data.EquivalentLenghPrice,
                Insulation = result.Data.Insulation,
                LaborDayPrice = result.Data.LaborDayPrice,



            };
            await LoadFromLocalStorage();
            SelectedBrand = Model.Brand;
        }
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
        SaveModelToLocalStorage().ContinueWith(_ =>
        {
            Navigation.NavigateTo(StaticClass.Brands.PageName.Create);
        });
    }
    private async Task SaveModelToLocalStorage()
    {
        await _localModelStorage.SaveToLocalStorage(Model);
    }
    async Task LoadFromLocalStorage()
    {
        Model = await _localModelStorage.LoadFromLocalStorage(Model) ?? Model;
    }
}
