using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Shared.Models.Backgrounds.Requests;
using Shared.Models.Cases.Requests;
using Shared.Models.Cases.Responses;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Projects.Reponses;
using Shared.Models.Requirements.Requests;
using Web.Infrastructure.Managers.Generic;

namespace Web.Pages.CreateUpdates.Requirements;
#nullable disable
public partial class CreateRequirement
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public DeliverableResponse Parent { get; set; }
    [Parameter]
    public Func<Task> GetAll { get; set; }
    [Parameter]
    public Action Cancel { get; set; }
    [Inject]
    private IGenericService Service { get; set; } = null!;
    CreateRequirementRequest Model { get; set; } = new();


    protected override void OnInitialized()
    {
        Model.DeliverableId = Parent.Id;
        Model.ProjectId = Parent.ProjectId;
    }
    private async Task SaveAsync()
    {
        var result = await Service.Create(Model);
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
    private bool Validated { get; set; }
    FluentValidationValidator _fluentValidationValidator = null!;
    async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }

}
