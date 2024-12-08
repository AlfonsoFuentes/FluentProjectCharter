using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Shared.Models.Assumptions.Requests;
using Shared.Models.Deliverables.Responses;
using Shared.Models.FileResults.Generics.Request;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.SharedComponents;
#nullable disable
public partial class CreateTemplate<TItem> where TItem : class, IRequest
{
    [CascadingParameter]
    public App App { get; set; }

    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    [Parameter]
    [EditorRequired]
    public Action Cancel { get; set; }
    [Inject]
    private IGenericService Service { get; set; } = null!;
    [Parameter]
    public TItem Model { get; set; }
    [Parameter]
    public EventCallback<TItem> ModelChanged { get; set; }
    [Parameter]
    [EditorRequired]
    public string Title { get; set; }

    [Parameter]
    [EditorRequired]
    public RenderFragment Content { get; set; }
    private async Task SaveAsync()
    {
        var result = await Service.Create(Model);
        if (result.Succeeded)
        {
                       
            await GetAll.Invoke();
            Cancel();
            _snackBar.ShowSuccess(result.Messages);
        }
        else
        {
            _snackBar.ShowError(result.Messages);


        }


    }
    private bool Validated { get; set; }
    FluentValidationValidator _fluentValidationValidator = null!;
    public async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }


}
