using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Shared.Models.Projects.Request;
using Web.Infrastructure.Managers.Generic;

#nullable disable
namespace Web.Pages.CreateUpdates.Projects;
public partial class CreateProject
{
    [CascadingParameter]
    public App App { get; set; }
    CreateProjectRequest Model { get; set; } = new();
    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
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
        Navigation.NavigateBack();

    }
    private bool Validated { get; set; }
    FluentValidationValidator _fluentValidationValidator = null!;
    async Task ValidateAsync()
    {
        Validated = _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }

}


