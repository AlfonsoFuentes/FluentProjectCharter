using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.OrganizationStrategies.Responses;
using Web.Infrastructure.Managers.Generic;
using Web.Infrastructure.Managers.OrganizationStrategys;
#nullable disable

namespace FluentWeb.Pages.OrganizationStrategys;
public partial class CreateOrganizacionStrategy
{
    [CascadingParameter]
    public App App { get; set; }

    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    [Parameter]
    [EditorRequired]
    public Action Cancel { get; set; }
    [Parameter]
    [EditorRequired]
    public Func<OrganizationStrategyResponse, bool> SendData { get; set; }

    [Inject]
    private IOrganizationStrategyService Service { get; set; } = null!;
    CreateOrganizationStrategyRequest Model { get; set; } = new();
    private async Task SaveAsync()
    {
        var result = await Service.Create(Model);
        if (result.Succeeded)
        {
            await GetAll.Invoke();
            SendData.Invoke(result.Data);
          
            
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
