using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Shared.Models.Backgrounds.Requests;
using Shared.Models.Backgrounds.Responses;
using Shared.Models.Cases.Requests;
using Shared.Models.Cases.Responses;
using Shared.Models.DeliverableRisks.Requests;
using Shared.Models.DeliverableRisks.Responses;
using Web.Infrastructure.Managers.Generic;

namespace Web.Pages.CreateUpdates.DeliverableRisks;
#nullable disable
public partial class UpdateDeliverableRisk
{
    [CascadingParameter]
    public App App { get; set; }

    UpdateDeliverableRiskRequest Model { get; set; } = new();
    [Parameter]
    public DeliverableRiskResponse Response { get; set; } = new();
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