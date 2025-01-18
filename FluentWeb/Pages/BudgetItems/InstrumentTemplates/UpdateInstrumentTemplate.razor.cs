
using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.Templates.Instruments.Records;
using Shared.Models.Templates.Instruments.Requests;
using Shared.Models.Templates.Instruments.Responses;
using Shared.StaticClasses;

namespace FluentWeb.Pages.BudgetItems.InstrumentTemplates;
public partial class UpdateInstrumentTemplate
{
    UpdateInstrumentTemplateRequest Model = new();
    [Parameter]
    public Guid Id { get; set; }
    BrandResponseList BrandsResponseList { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        await GetBrands();
        var result = await GenericService.GetById<InstrumentTemplateResponse, GetInstrumentTemplateByIdRequest>(
           new GetInstrumentTemplateByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,

                BrandResponse = result.Data.BrandResponse,

                Type = result.Data.Type,
                Material = result.Data.Material,
                Model = result.Data.Model,
                Reference = result.Data.Reference,
                SignalType = result.Data.SignalType,
                Value = result.Data.Value,
                Nozzles = result.Data.Nozzles,
                SubType = result.Data.SubType,
                


            };
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
        Navigation.NavigateTo(StaticClass.Brands.PageName.Create);
    }
}
