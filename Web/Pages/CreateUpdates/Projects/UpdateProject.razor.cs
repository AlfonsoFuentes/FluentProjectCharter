using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Shared.Models.Projects.Records;
using Shared.Models.Projects.Reponses;
using Shared.Models.Projects.Request;
using Web.Infrastructure.Managers.Generic;

#nullable disable

namespace Web.Pages.CreateUpdates.Projects;
public partial class UpdateProject
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public ProjectResponse Response { get; set; }
    public UpdateProjectRequest Model = new();
    [Inject]
    private IGenericService GenericService { get; set; } = null!;
    [Parameter]
    public Func<Task> GetAll { get; set; }
    [Parameter]
    public Action Cancel { get; set; }
    protected override void OnInitialized()
    {
        Model.Id = Response.Id;
        Model.Name = Response.Name;
    }

    private async Task SaveAsync()
    {


        var result = await GenericService.Update(Model);
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
        Cancel.Invoke();
        StateHasChanged();


    }
    private bool Validated { get; set; }
    FluentValidationValidator _fluentValidationValidator = null!;
    async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }
}
