using Microsoft.AspNetCore.Components;
using Shared.Enums.NozzleTypes;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.BudgetItems.IndividualItems.Pipes.Requests;
using Shared.Models.EngineeringFluidCodes.Records;
using Shared.Models.EngineeringFluidCodes.Responses;
using Shared.Models.Templates.Pipings.Records;
using Shared.Models.Templates.Pipings.Responses;
using Shared.StaticClasses;
#nullable disable
namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Pipes;
public partial class CreatePipe
{
    [CascadingParameter]
    public App App { get; set; }
    CreatePipeRequest Model = new();

    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid? GanttTaskId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetAllPipeTemplate();
        await GetBrands();
        await GetEngineeringFluidCode();
        Model.GanttTaskId = GanttTaskId;
        Model.ProjectId = ProjectId;
        Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Inlet });
        Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Outlet });
        await LoadFromLocalStorage();
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
        SelectedBrand = Model.Brand;
        Model.Nozzles.ForEach(x => x.NominalDiameter = Model.Diameter);
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
