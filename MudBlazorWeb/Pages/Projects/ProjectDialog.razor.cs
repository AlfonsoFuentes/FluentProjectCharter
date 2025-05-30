using Blazored.FluentValidation;
using MudBlazor;
using Shared.Models.Brands.Responses;
using Shared.Models.Projects.Reponses;

namespace MudBlazorWeb.Pages.Projects;
public partial class ProjectDialog
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = null!;
    private bool Validated { get; set; } = false;
    async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }
    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
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
    public ProjectResponse Model { get; set; } = new();

}
