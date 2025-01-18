using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.Pipes.Records;
using Shared.Models.BudgetItems.Pipes.Requests;
using Shared.Models.BudgetItems.Pipes.Responses;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Shared.Models.Templates.Pipes.Records;
using Shared.Models.Templates.Pipes.Responses;
using Shared.StaticClasses;
using Shared.Models.OrganizationStrategies.Records;
using Shared.Models.OrganizationStrategies.Responses;

namespace FluentWeb.Pages.BudgetItems.Pipes;
public partial class UpdatePipe
{
    UpdatePipeRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await GetAllPipeTemplate();
        await GetBrands();
        await GetEngineeringFluidCode();
        var result = await GenericService.GetById<PipeResponse, GetPipeByIdRequest>(
            new GetPipeByIdRequest() { Id = Id, ProjectId = ProjectId });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = ProjectId,
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




            };
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
        Navigation.NavigateTo(StaticClass.Brands.PageName.Create);
    }
    void AddFluidCode()
    {
        Navigation.NavigateTo(StaticClass.EngineeringFluidCodes.PageName.Create);
    }

}
