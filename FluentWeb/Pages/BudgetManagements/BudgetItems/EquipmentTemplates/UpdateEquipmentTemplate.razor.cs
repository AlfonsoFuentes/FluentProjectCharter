
using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.Templates.Equipments.Records;
using Shared.Models.Templates.Equipments.Requests;
using Shared.Models.Templates.Equipments.Responses;
using Shared.StaticClasses;

namespace FluentWeb.Pages.BudgetManagements.BudgetItems.EquipmentTemplates;
public partial class UpdateEquipmentTemplate
{
    UpdateEquipmentTemplateRequest Model = new();
    [Parameter]
    public Guid Id { get; set; }
    BrandResponseList BrandsResponseList { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        await GetBrands();
        var result = await GenericService.GetById<EquipmentTemplateResponse, GetEquipmentTemplateByIdRequest>(
           new GetEquipmentTemplateByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,

                ExternalMaterial = result.Data.ExternalMaterial,
                Reference = result.Data.Reference,
                Model = result.Data.Model,
                BrandResponse = result.Data.BrandResponse,
                InternalMaterial = result.Data.InternalMaterial,

                SubType = result.Data.SubType,
                Type = result.Data.Type,
                TagLetter = result.Data.TagLetter,
                Value = result.Data.Value,
                Nozzles = result.Data.Nozzles,


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
