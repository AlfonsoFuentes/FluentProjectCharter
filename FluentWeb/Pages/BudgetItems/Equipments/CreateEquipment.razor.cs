using FluentWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.Equipments.Requests;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Shared.Models.Templates.Equipments.Records;
using Shared.Models.Templates.Equipments.Responses;
using Shared.StaticClasses;
using System.Text.Json;
#nullable disable

namespace FluentWeb.Pages.BudgetItems.Equipments;
public partial class CreateEquipment
{
    CreateEquipmentRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid DeliverableId { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await GetAllEquipmentTemplate();
        await GetBrands();
        await LoadFromLocalStorage();
        SelectedBrand = Model.Brand;
        Model.ProjectId = ProjectId;
        Model.DeliverableId = DeliverableId;

    }
   

    EquipmentTemplateResponseList EquipmentTemplateResponseList = new();
    async Task GetAllEquipmentTemplate()
    {
        var result = await GenericService.GetAll<EquipmentTemplateResponseList, EquipmentTemplateGetAll>(new EquipmentTemplateGetAll());
        if (result.Succeeded)
        {
            EquipmentTemplateResponseList = result.Data;
        }
    }

    BrandResponseList BrandsResponseList { get; set; } = new();
    void GetFromTamplateList(EquipmentTemplateResponse response)
    {
        Model.BrandResponse = response.BrandResponse;
        Model.Reference = response.Reference;
        Model.ExternalMaterial = response.ExternalMaterial;
        Model.InternalMaterial = response.InternalMaterial;
        Model.Model = response.Model;

        Model.SubType = response.SubType;
        Model.TagLetter = response.TagLetter;
        Model.Type = response.Type;

        Model.TagLetter = response.TagLetter;

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
        Model = await _localModelStorage.LoadFromLocalStorage(Model) ?? new();
    }

}
