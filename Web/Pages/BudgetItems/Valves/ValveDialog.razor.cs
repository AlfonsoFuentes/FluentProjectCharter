using Blazored.FluentValidation;
using MudBlazor;
using Shared.Enums.NozzleTypes;
using Shared.Enums.ValvesEnum;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.BudgetItems.IndividualItems.Valves.Records;
using Shared.Models.BudgetItems.IndividualItems.Valves.Responses;
using Shared.Models.Templates.NozzleTemplates;
using Shared.Models.Templates.Valves.Records;
using Shared.Models.Templates.Valves.Responses;
using Web.Pages.Brands;

namespace Web.Pages.BudgetItems.Valves;
public partial class ValveDialog
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = null!;
    private bool Validated { get; set; } = false;
    async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }


    FluentValidationValidator _fluentValidationValidator = null!;
    BrandResponseList BrandsResponseList { get; set; } = new();
    async Task GetBrands()
    {
        var result = await GenericService.GetAll<BrandResponseList, BrandGetAll>(new BrandGetAll());
        if (result.Succeeded)
        {
            BrandsResponseList = result.Data;
        }
    }
    ValveTemplateResponseList ValveTemplateResponseList = new();
    async Task GetAllValveTemplate()
    {
        var result = await GenericService.GetAll<ValveTemplateResponseList, ValveTemplateGetAll>(new ValveTemplateGetAll());
        if (result.Succeeded)
        {
            ValveTemplateResponseList = result.Data;
        }
    }
    async Task GetValveResponse()
    {
        if (Model.Id != Guid.Empty)
        {
            var result = await GenericService.GetById<ValveResponse, GetValveByIdRequest>(
                  new GetValveByIdRequest() { Id = Model.Id });

            if (result.Succeeded)
            {
                Model = result.Data;
                await OnChageDetails();
            }

        }


    }
    bool loaded = false;
    protected override async Task OnInitializedAsync()
    {
        await GetAllValveTemplate();
        await GetBrands();
        await GetValveResponse();
        StateHasChanged();
        loaded = true;
    }
    private async Task Submit()
    {
        var result = await GenericService.Post(Model);


        if (result.Succeeded)
        {
            _snackBar.ShowSuccess(result.Messages);
            MudDialog.Close(DialogResult.Ok(true));
        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }

    }


    private void Cancel() => MudDialog.Cancel();

    [Parameter]
    public ValveResponse Model { get; set; } = new();

    async Task AddBrand()
    {
        var parameters = new DialogParameters<BrandDialog>
        {

        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Small };

        var dialog = await DialogService.ShowAsync<BrandDialog>("Brand", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetBrands();
            StateHasChanged();
        }
    }
    async Task GetFromTamplateList(ValveTemplateResponse response)
    {
        Model.Brand = response.Brand!;

        Model.ActuatorType = response.ActuatorType;
        Model.Diameter = response.Diameter;
        Model.FailType = response.FailType;
        Model.HasFeedBack = response.HasFeedBack;
        Model.SignalType = response.SignalType;
        Model.PositionerType = response.PositionerType;
        Model.Model = response.Model;
        Model.Material = response.Material;
        Model.Type = response.Type;
        Model.HasFeedBack = response.HasFeedBack;
        Model.Nozzles = response.Nozzles.Select((row, index) => new NozzleResponse
        {
            Order = index + 1,
            Id = Guid.NewGuid(),
            ConnectionType = row.ConnectionType,
            NominalDiameter = row.NominalDiameter,
            NozzleType = row.NozzleType
        }).ToList();
        Model.BudgetUSD = response.Value;

        await ValidateAsync();
    }
    void ChangValveType()
    {

        // Actualizar las boquillas según el tipo de válvula
        UpdateNozzlesBasedOnValveType();


    }

    void UpdateNozzlesBasedOnValveType()
    {
        if (Model.Nozzles.Count == 0)
        {
            AddInitialNozzles();
        }
        else
        {
            AdjustNozzlesForValveType();
        }
    }

    void AddInitialNozzles()
    {
        Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Inlet, NominalDiameter = Model.Diameter,  });
        Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Outlet, NominalDiameter = Model.Diameter, });

        if (Model.Type == ValveTypesEnum.Ball_Three_Way_L || Model.Type == ValveTypesEnum.Ball_Three_Way_T)
        {
            Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Outlet, NominalDiameter = Model.Diameter,  });
        }
        else if (Model.Type == ValveTypesEnum.Diaphragm_Zero_deadLeg || Model.Type == ValveTypesEnum.Ball_Zero_deadLeg)
        {
            Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Outlet,  });
        }
        else if (Model.Type == ValveTypesEnum.Ball_Four_Way)
        {
            Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Outlet, NominalDiameter = Model.Diameter, });
            Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Outlet, NominalDiameter = Model.Diameter,  });
        }
    }

    void AdjustNozzlesForValveType()
    {
        if (Model.Type == ValveTypesEnum.Ball_Three_Way_L || Model.Type == ValveTypesEnum.Ball_Three_Way_T)
        {

            if (Model.Nozzles.Count == 2)
            {
                Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Outlet, NominalDiameter = Model.Diameter });
            }
            else if (Model.Nozzles.Count == 4)
            {
                Model.Nozzles.Remove(Model.Nozzles.Last());
            }
        }
        else if (Model.Type == ValveTypesEnum.Diaphragm_Zero_deadLeg || Model.Type == ValveTypesEnum.Ball_Zero_deadLeg)
        {
            if (Model.Nozzles.Count == 2)
            {
                Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Outlet });
            }
            else if (Model.Nozzles.Count == 4)
            {
                Model.Nozzles.Remove(Model.Nozzles.Last());
            }
        }
        else if (Model.Type == ValveTypesEnum.Ball_Four_Way)
        {
            if (Model.Nozzles.Count == 2)
            {
                Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Outlet, NominalDiameter = Model.Diameter });
                Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Outlet, NominalDiameter = Model.Diameter });
            }
            else if (Model.Nozzles.Count == 3)
            {
                Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Outlet, NominalDiameter = Model.Diameter });
            }

        }
        else if (Model.Nozzles.Count == 3)
        {
            Model.Nozzles.Remove(Model.Nozzles.Last());
        }
        else if (Model.Nozzles.Count == 4)
        {
            Model.Nozzles.Remove(Model.Nozzles.Last());
            Model.Nozzles.Remove(Model.Nozzles.Last());
        }
    }


    void ChangeDiameter()
    {

        if (Model.Nozzles.Count > 0)
        {
            foreach (var nozzle in Model.Nozzles)
            {
                nozzle.NominalDiameter = Model.Diameter;
            }
        }

    }
   
    void ChangeActuator()
    {

        if (Model.ActuatorType.Id == ActuatorTypeEnum.Hand.Id)
        {
            Model.SignalType = SignalTypeEnum.NotApplicable;
            Model.FailType = FailTypeEnum.Not_Applicable;
        }
        if (Model.ActuatorType.Id == ActuatorTypeEnum.Double_effect.Id)
        {

            Model.FailType = FailTypeEnum.Not_Applicable;
        }
    }
    void ChangePositioner()
    {

        if (Model.PositionerType == PositionerTypeEnum.Proportional)
        {
            Model.SignalType = SignalTypeEnum.mA_4_20;
        }
        else
        {
            Model.SignalType = SignalTypeEnum.None;
        }

    }
    async Task OnChageDetails()
    {
        if (Model.ShowDetails)
        {
            await this.MudDialog.SetOptionsAsync(new DialogOptions() { MaxWidth = MaxWidth.Large });
        }
        else
        {
            await this.MudDialog.SetOptionsAsync(new DialogOptions() { MaxWidth = MaxWidth.Medium });
        }
    }
}
