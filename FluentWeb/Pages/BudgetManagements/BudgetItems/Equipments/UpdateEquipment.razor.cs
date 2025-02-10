using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Records;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Requests;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.Templates.Equipments.Records;
using Shared.Models.Templates.Equipments.Responses;
using Shared.StaticClasses;


namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Equipments;
public partial class UpdateEquipment
{
    UpdateEquipmentRequest Model = new();

    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await GetAllEquipmentTemplate();
        await GetBrands();

        var result = await GenericService.GetById<EquipmentResponse, GetEquipmentByIdRequest>(
            new GetEquipmentByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = result.Data.ProjectId,
                Budget = result.Data.Budget,
                BrandResponse = result.Data.BrandResponse,
                DeliverableId = result.Data.DeliverableId,
                ExternalMaterial = result.Data.ExternalMaterial,
                InternalMaterial = result.Data.InternalMaterial,
                Model = result.Data.Model,
                Reference = result.Data.Reference,
                SubType = result.Data.SubType,
                TagLetter = result.Data.TagLetter,
                Type = result.Data.Type,
                Nozzles = result.Data.Nozzles,
                IsExisting = result.Data.IsExisting,

                TagNumber = result.Data.TagNumber,
                ShowDetails = result.Data.ShowDetails,
                ShowProvisionalTag = result.Data.ShowProvisionalTag,
                ProvisionalTag = result.Data.ProvisionalTag,


            };
            await LoadFromLocalStorage();
            SelectedBrand = Model.Brand;
        }
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
        Model.Budget = response.Value;
        Model.Nozzles = response.Nozzles.Select((row, index) => new NozzleResponse
        {
            Order = index + 1,
            Id = Guid.NewGuid(),
            ConnectionType = row.ConnectionType,
            NominalDiameter = row.NominalDiameter,
            NozzleType = row.NozzleType
        }).ToList();
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
        Model = await _localModelStorage.LoadFromLocalStorage(Model) ?? Model;
    }

}
