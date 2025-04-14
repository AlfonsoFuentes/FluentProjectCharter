using Blazored.FluentValidation;
using MudBlazor;
using Shared.Models.Assumptions.Responses;

namespace Web.Pages.ProjectDependant.Assumptions;
public partial class AssumptionDialog
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = null!;
    private bool Validated { get; set; }
    // M�todo asincr�nico para realizar la validaci�n
    public async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
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
    public AssumptionResponse Model { get; set; } = new();

}
