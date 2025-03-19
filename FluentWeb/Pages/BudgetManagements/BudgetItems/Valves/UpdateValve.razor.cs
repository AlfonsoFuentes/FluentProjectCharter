using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.BudgetItems.IndividualItems.Valves.Records;
using Shared.Models.BudgetItems.IndividualItems.Valves.Requests;
using Shared.Models.BudgetItems.IndividualItems.Valves.Responses;
using Shared.Models.Templates.Valves.Records;
using Shared.Models.Templates.Valves.Responses;
using Shared.StaticClasses;

namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Valves;
public partial class UpdateValve
{
    UpdateValveRequest Model = new();

    [Parameter]
    public Guid Id { get; set; }
    BrandResponseList BrandsResponseList { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        await GetAllEquipmentTemplate();
        await GetBrands();
        var result = await GenericService.GetById<ValveResponse, GetValveByIdRequest>(
            new GetValveByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = result.Data.ProjectId,
                BudgetUSD = result.Data.BudgetUSD,

                TagNumber = result.Data.TagNumber,

                Type = result.Data.Type,
                SignalType = result.Data.SignalType,
                FailType = result.Data.FailType,
                Diameter = result.Data.Diameter,
                HasFeedBack = result.Data.HasFeedBack,
                PositionerType = result.Data.PositionerType,

                Material = result.Data.Material,
                Model = result.Data.Model,
                ActuatorType = result.Data.ActuatorType,
                BrandResponse = result.Data.BrandResponse,
                GanttTaskId = result.Data.DeliverableId,
                Nozzles = result.Data.Nozzles,

                ShowDetails = result.Data.ShowDetails,
                IsExisting= result.Data.IsExisting,
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
  
    ValveTemplateResponseList ValveTemplateResponseList = new();
    async Task GetAllEquipmentTemplate()
    {
        var result = await GenericService.GetAll<ValveTemplateResponseList, ValveTemplateGetAll>(new ValveTemplateGetAll());
        if (result.Succeeded)
        {
            ValveTemplateResponseList = result.Data;
        }
    }
    void GetFromTamplateList(ValveTemplateResponse response)
    {
        Model.BrandResponse = response.BrandResponse;
        Model.ActuatorType = response.ActuatorType;
        Model.Diameter = response.Diameter;
        Model.FailType = response.FailType;
        Model.HasFeedBack = response.HasFeedBack;
        Model.SignalType = response.SignalType;
        Model.PositionerType = response.PositionerType;
        Model.Model = response.Model;
        Model.Material = response.Material;


        Model.Nozzles = response.Nozzles.Select((row, index) => new NozzleResponse
        {
            Order = index + 1,
            Id = Guid.NewGuid(),
            ConnectionType = row.ConnectionType,
            NominalDiameter = row.NominalDiameter,
            NozzleType = row.NozzleType
        }).ToList();
        Model.BudgetUSD = response.Value;
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
