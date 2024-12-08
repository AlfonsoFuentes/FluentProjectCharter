using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Shared.Models.Requirements.Requests;
using Shared.Models.Requirements.Responses;
using Web.Infrastructure.Managers.Generic;

namespace Web.Pages.CreateUpdates.Requirements;
#nullable disable
public partial class UpdateRequirement
{
    [CascadingParameter]
    public App App { get; set; }

    UpdateRequirementRequest Model { get; set; } = new();
    [Parameter]
    public RequirementResponse Response { get; set; } = new();
    [Inject]
    private IGenericService Service { get; set; } = null!;
    [Parameter]
    public Func<Task> GetAll { get; set; }
    [Parameter]
    public Action Cancel { get; set; }
    protected override void OnInitialized()
    {
        Model.Id = Response.Id;
        Model.Name = Response.Name;
        Model.ProjectId = Response.ProjectId;
        Model.DeliverableId = Response.DeliverableId;

    }
    private async Task SaveAsync()
    {
        var result = await Service.Update(Model);
        if (result.Succeeded)
        {
            _snackBar.Add(result.Messages, Radzen.NotificationSeverity.Success);
            CancelAsync();
            await GetAll.Invoke();
        }
        else
        {
            _snackBar.Add(result.Messages, Radzen.NotificationSeverity.Error);


        }


    }

    private void CancelAsync()
    {
        Cancel();

    }
    private bool Validated { get; set; } = true;
    FluentValidationValidator _fluentValidationValidator = null!;
    async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }
}
