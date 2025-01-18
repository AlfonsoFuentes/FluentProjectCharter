using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Shared.Models.BudgetItems.Instruments.Requests;
using Shared.Models.Templates.Equipments.Records;
using Shared.Models.Templates.Equipments.Responses;
using Shared.Models.Templates.Instruments.Records;
using Shared.Models.Templates.Instruments.Responses;
using Shared.StaticClasses;

namespace FluentWeb.Pages.BudgetItems.Instruments;
public partial class CreateInstrument
{
    CreateInstrumentRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid DeliverableId { get; set; }
    BrandResponseList BrandsResponseList { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        Model.ProjectId = ProjectId;
        Model.DeliverableId = DeliverableId;
        await GetAllEquipmentTemplate();
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
    InstrumentTemplateResponseList InstrumentTemplateResponseList = new();
    async Task GetAllEquipmentTemplate()
    {
        var result = await GenericService.GetAll<InstrumentTemplateResponseList, InstrumentTemplateGetAll>(new InstrumentTemplateGetAll());
        if (result.Succeeded)
        {
            InstrumentTemplateResponseList = result.Data;
        }
    }
    void GetFromTamplateList(InstrumentTemplateResponse response)
    {
        Model.BrandResponse = response.BrandResponse;
        Model.SignalType = response.SignalType;
        Model.Model = response.Model;
        Model.Material = response.Material;
        Model.Reference = response.Reference;
        Model.Type = response.Type;
        Model.SubType = response.SubType;
        

        Model.Nozzles = response.Nozzles.Select((row, index) => new NozzleResponse
        {
            Order = index + 1,
            Id = Guid.NewGuid(),
            ConnectionType = row.ConnectionType,
            NominalDiameter = row.NominalDiameter,
            NozzleType = row.NozzleType
        }).ToList();
        Model.Budget = response.Value;
        SelectedBrand = Model.Brand;
        StateHasChanged();
    }
}
