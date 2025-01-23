using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Shared.Models.BudgetItems.Valves.Requests;
using Shared.Models.Templates.Equipments.Records;
using Shared.Models.Templates.Equipments.Responses;
using Shared.Models.Templates.Valves.Records;
using Shared.Models.Templates.Valves.Responses;
using Shared.StaticClasses;

namespace FluentWeb.Pages.BudgetItems.Valves;
public partial class CreateValve
{
    CreateValveRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid DeliverableId { get; set; }
    BrandResponseList BrandsResponseList { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        await LoadFromLocalStorage();
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
        Model.ActuatorType=response.ActuatorType;
        Model.Diameter=response.Diameter;
        Model.FailType=response.FailType;
        Model.HasFeedBack=response.HasFeedBack;
        Model.SignalType=response.SignalType;
        Model.PositionerType=response.PositionerType;
        Model.Model=response.Model;
        Model.Material=response.Material;
        

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
        Model = await _localModelStorage.LoadFromLocalStorage(Model) ?? new();
    }
}
