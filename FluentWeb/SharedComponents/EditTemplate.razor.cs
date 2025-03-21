using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Shared.Models.Assumptions.Requests;
using Shared.Models.Deliverables.Responses;
using Shared.Models.FileResults.Generics.Request;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.SharedComponents;
#nullable disable
public partial class EditTemplate<TItem> where TItem : class, IRequest
{
    [CascadingParameter]
    public App App { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    [Parameter]
    public TItem Model { get; set; }
    [Parameter]
    public EventCallback<TItem> ModelChanged { get; set; }
    [Parameter]
    public string Title { get; set; }=string.Empty;

    [Parameter]
    [EditorRequired]
    public RenderFragment Content { get; set; }
    private async Task SaveAsync()
    {
        var result = await Service.Update(Model);
        if (result.Succeeded)
        {
            _snackBar.ShowSuccess(result.Messages);
            if (GoBack)
                Cancel();

        }
        else
        {
            _snackBar.ShowError(result.Messages);


        }


    }
    void Cancel()
    {
        Navigation.NavigateBack();
    }

    private bool Validated { get; set; }
    FluentValidationValidator _fluentValidationValidator = null!;
    public async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }
    [Parameter]
    public bool GoBack { get; set; } = true;
}
