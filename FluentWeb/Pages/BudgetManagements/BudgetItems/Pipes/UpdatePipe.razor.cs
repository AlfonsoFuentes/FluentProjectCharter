using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Pipes.Records;
using Shared.Models.BudgetItems.IndividualItems.Pipes.Requests;
using Shared.Models.BudgetItems.IndividualItems.Pipes.Responses;
using Shared.Models.EngineeringFluidCodes.Records;
using Shared.Models.EngineeringFluidCodes.Responses;
using Shared.Models.Templates.Pipings.Records;
using Shared.Models.Templates.Pipings.Responses;
using Shared.StaticClasses;

namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Pipes;
public partial class UpdatePipe
{
    UpdatePipeRequest Model = new();
 
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await GetAllPipeTemplate();
        await GetBrands();
        await GetEngineeringFluidCode();
        var result = await GenericService.GetById<PipeResponse, GetPipeByIdRequest>(
            new GetPipeByIdRequest() { Id = Id});

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
                Diameter = result.Data.Diameter,
                LaborDayPrice = result.Data.LaborDayPrice,
                Nozzles = result.Data.Nozzles,
                TagNumber = result.Data.TagNumber,
                ShowDetails = result.Data.ShowDetails,
                EquivalentLenghPrice = result.Data.EquivalentLenghPrice,
                LaborQuantity = result.Data.LaborQuantity,
                Insulation = result.Data.Insulation,
                Material = result.Data.Material,
                MaterialQuantity = result.Data.MaterialQuantity,
                PipeClass = result.Data.PipeClass,
                FluidCode = result.Data.FluidCode,
                IsExisting=result.Data.IsExisting,  



            };
            await LoadFromLocalStorage();
            SelectedBrand = Model.Brand;
            SelectedFluid = Model.FluidCodeName;
        }
    }
    PipeTemplateResponseList PipeTemplateResponseList = new();
    async Task GetAllPipeTemplate()
    {
        var result = await GenericService.GetAll<PipeTemplateResponseList, PipeTemplateGetAll>(new PipeTemplateGetAll());
        if (result.Succeeded)
        {
            PipeTemplateResponseList = result.Data;
        }
    }

    BrandResponseList BrandsResponseList { get; set; } = new();
    void GetFromTamplateList(PipeTemplateResponse response)
    {
        Model.BrandResponse = response.BrandResponse;
        Model.Diameter = response.Diameter;
        Model.PipeClass = response.Class;
        Model.Material = response.Material;
        Model.LaborDayPrice = response.LaborDayPrice;
        Model.Insulation = response.Insulation;
        Model.EquivalentLenghPrice = response.EquivalentLenghPrice;
        Model.Nozzles.ForEach(x => x.NominalDiameter = Model.Diameter);

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
    EngineeringFluidCodeResponseList EngineeringFluidCodeResponseList = new();
    async Task GetEngineeringFluidCode()
    {
        var result = await GenericService.GetAll<EngineeringFluidCodeResponseList, EngineeringFluidCodeGetAll>(new EngineeringFluidCodeGetAll());
        if (result.Succeeded)
        {
            EngineeringFluidCodeResponseList = result.Data;
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
    void AddFluidCode()
    {
        SaveModelToLocalStorage().ContinueWith(_ =>
        {
            Navigation.NavigateTo(StaticClass.EngineeringFluidCodes.PageName.Create);
        });


    }

}
