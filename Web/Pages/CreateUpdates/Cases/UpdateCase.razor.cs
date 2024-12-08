using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Requests;
using Shared.Models.Cases.Responses;
using Web.Infrastructure.Managers.Generic;
#nullable disable
namespace Web.Pages.CreateUpdates.Cases;
public partial class UpdateCase
{
    [CascadingParameter]
    public App App { get; set; }

    UpdateCaseRequest Model { get; set; } = new();
    [Parameter]
    public CaseResponse Response { get; set; } = new();
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
