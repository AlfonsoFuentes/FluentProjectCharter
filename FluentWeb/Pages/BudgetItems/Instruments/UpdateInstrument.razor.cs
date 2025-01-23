using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Shared.Models.BudgetItems.Instruments.Records;
using Shared.Models.BudgetItems.Instruments.Requests;
using Shared.Models.BudgetItems.Instruments.Responses;
using Shared.Models.Templates.Instruments.Records;
using Shared.Models.Templates.Instruments.Responses;
using Shared.StaticClasses;

namespace FluentWeb.Pages.BudgetItems.Instruments;
public partial class UpdateInstrument
{
    UpdateInstrumentRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    BrandResponseList BrandsResponseList { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        await GetAllEquipmentTemplate();
        await GetBrands();
        var result = await GenericService.GetById<InstrumentResponse, GetInstrumentByIdRequest>(
            new GetInstrumentByIdRequest() { Id = Id, ProjectId = ProjectId });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = ProjectId,
                Budget = result.Data.Budget,

                TagNumber = result.Data.TagNumber,

                Type = result.Data.Type,
                SignalType = result.Data.SignalType,

                Material = result.Data.Material,
                Model = result.Data.Model,

                BrandResponse = result.Data.BrandResponse,
                DeliverableId = result.Data.DeliverableId,
                Nozzles = result.Data.Nozzles,

                ShowDetails = result.Data.ShowDetails,
                Reference = result.Data.Reference,
                SubType = result.Data.SubType,
                IsExisting = result.Data.IsExisting,
                ShowProvisionalTag = result.Data.ShowProvisionalTag,
                ProvisionalTag = result.Data.ProvisionalTag,
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
