using Blazored.FluentValidation;
using MudBlazor;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Records;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.Templates.Equipments.Records;
using Shared.Models.Templates.Equipments.Responses;
using System.Threading.Tasks;
using Web.Pages.Brands;

namespace Web.Pages.BudgetItems.Equipments;
public partial class EquipmentDialog
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = null!;
    DialogOptions _dialogOptions => MudDialog.Options;
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
    EquipmentTemplateResponseList EquipmentTemplateResponseList = new();
    async Task GetAllEquipmentTemplate()
    {
        var result = await GenericService.GetAll<EquipmentTemplateResponseList, EquipmentTemplateGetAll>(new EquipmentTemplateGetAll());
        if (result.Succeeded)
        {
            EquipmentTemplateResponseList = result.Data;
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
    async Task GetEquipmentResponse()
    {
        if (Model.Id!=Guid.Empty)
        {
            var result = await GenericService.GetById<EquipmentResponse, GetEquipmentByIdRequest>(
                  new GetEquipmentByIdRequest() { Id = Model.Id });

            if (result.Succeeded)
            {
                Model = result.Data;
                await OnChageDetails();



            }

        }
       

    }
    bool loaded=false;
    protected override async Task OnInitializedAsync()
    {
        await GetAllEquipmentTemplate();
        await GetBrands();
        await GetEquipmentResponse();   
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
    public EquipmentResponse Model { get; set; } = new();
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
    async Task GetFromTamplateList(EquipmentTemplateResponse response)
    {
        Model.Brand = response.Brand!;
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
        Model.BudgetUSD = response.Value;

        await ValidateAsync();
    }
}
