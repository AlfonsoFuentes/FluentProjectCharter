
using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.Templates.Valves.Records;
using Shared.Models.Templates.Valves.Requests;
using Shared.Models.Templates.Valves.Responses;
using Shared.StaticClasses;

namespace FluentWeb.Pages.BudgetItems.ValveTemplates;
public partial class UpdateValveTemplate
{
    UpdateValveTemplateRequest Model = new();
    [Parameter]
    public Guid Id { get; set; }
    BrandResponseList BrandsResponseList { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        await GetBrands();
        var result = await GenericService.GetById<ValveTemplateResponse, GetValveTemplateByIdRequest>(
           new GetValveTemplateByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ActuatorType = result.Data.ActuatorType,
                BrandResponse = result.Data.BrandResponse,
                Diameter = result.Data.Diameter,
                Type = result.Data.Type,
                FailType = result.Data.FailType,
                HasFeedBack = result.Data.HasFeedBack,
                Material = result.Data.Material,
                Model = result.Data.Model,
                PositionerType = result.Data.PositionerType,
                SignalType = result.Data.SignalType,
                

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
