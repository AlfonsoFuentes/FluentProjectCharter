using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.Equipments.Requests;
using Shared.Models.Templates.Equipments.Requests;
using Shared.Models.Temporarys.Requests;
using Shared.StaticClasses;

namespace FluentWeb.Pages.BudgetItems.EquipmentTemplates;
public partial class CreateEquipmentTemplate
{
    CreateEquipmentTemplateRequest Model = new();

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
        //    Reference = Model.Reference,
        //    InternalMaterial = Model.InternalMaterial,
        //    ExternalMaterial = Model.ExternalMaterial,
        //    Value = Model.Value,
        //    Type = Model.Type,
        //    SubType = Model.SubType,
        //    TagLetter = Model.TagLetter,
        //    BrandTemplateId = Model.BrandResponse?.Id,
        //    EquipmentTemplate = true,

        //};
        //var result = await GenericService.Create(temporaryRequest);

        Navigation.NavigateTo(StaticClass.Brands.PageName.Create);
    }
}
