using Blazored.FluentValidation;
using MudBlazor;
using Shared.Enums.Instruments;
using Shared.Enums.NozzleTypes;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Instruments.Records;
using Shared.Models.BudgetItems.IndividualItems.Instruments.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.Templates.Instruments.Records;
using Shared.Models.Templates.Instruments.Responses;
using Shared.Models.Templates.NozzleTemplates;
using Web.Pages.Brands;

namespace Web.Pages.BudgetItems.Instruments;
public partial class InstrumentDialog
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = null!;
    private bool Validated { get; set; } = false;
    async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }
    BrandResponseList BrandsResponseList { get; set; } = new();
    async Task GetBrands()
    {
        var result = await GenericService.GetAll<BrandResponseList, BrandGetAll>(new BrandGetAll());
        if (result.Succeeded)
        {
            BrandsResponseList = result.Data;
        }
    }
    InstrumentTemplateResponseList InstrumentTemplateResponseList = new();
    async Task GetAllInstrumentTemplate()
    {
        var result = await GenericService.GetAll<InstrumentTemplateResponseList, InstrumentTemplateGetAll>(new InstrumentTemplateGetAll());
        if (result.Succeeded)
        {
            InstrumentTemplateResponseList = result.Data;
        }
    }
    async Task GetInstrumentResponse()
    {
        if (Model.Id != Guid.Empty)
        {
            var result = await GenericService.GetById<InstrumentResponse, GetInstrumentByIdRequest>(
                  new GetInstrumentByIdRequest() { Id = Model.Id });

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
        await GetAllInstrumentTemplate();
        await GetBrands();
        await GetInstrumentResponse();
        StateHasChanged();
        loaded = true;
    }

    FluentValidationValidator _fluentValidationValidator = null!;

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
    public InstrumentResponse Model { get; set; } = new();
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
    async Task GetFromTamplateList(InstrumentTemplateResponse response)
    {
        Model.Brand = response.Brand!;
        Model.SignalType = response.SignalType;
        Model.Model = response.Model;
        Model.Material = response.Material;
        Model.Reference = response.Reference;
        Model.VariableInstrument = response.VariableInstrument;
        Model.ModifierVariable = response.ModifierVariable;
        Model.ConnectionType = response.ConnectionType;

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
    void ChangeVariableInstrument()
    {
        // Actualizar las boquillas según el tipo de válvula
        UpdateNozzlesBasedOnInstrumentType();


    }


    void UpdateNozzlesBasedOnInstrumentType()
    {
        if (Model.Nozzles.Count == 0)
        {
            AddInitialNozzles();
        }
        else
        {
            AdjustNozzlesForInstrumentType();
        }
    }

    void AddInitialNozzles()
    {
        Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Inlet });

        if (Model.VariableInstrument == VariableInstrumentEnum.MassFlowMeter || Model.VariableInstrument == VariableInstrumentEnum.VolumeFlowMeter)
        {
            Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Outlet });
        }

    }
    void AdjustNozzlesForInstrumentType()
    {
        if (Model.VariableInstrument == VariableInstrumentEnum.MassFlowMeter || Model.VariableInstrument == VariableInstrumentEnum.VolumeFlowMeter)
        {
            if (Model.Nozzles.Count == 1)
            {
                Model.Nozzles.Add(new NozzleResponse() { Id = Guid.NewGuid(), NozzleType = NozzleTypeEnum.Outlet });
            }

        }
        else if (Model.Nozzles.Count == 2)
        {
            Model.Nozzles.Remove(Model.Nozzles.Last());
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
