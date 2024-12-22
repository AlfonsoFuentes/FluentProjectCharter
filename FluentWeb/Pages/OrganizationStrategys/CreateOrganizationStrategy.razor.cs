using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.OrganizationStrategies.Responses;
using Web.Infrastructure.Managers.Generic;
using Web.Infrastructure.Managers.OrganizationStrategys;
#nullable disable

namespace FluentWeb.Pages.OrganizationStrategys;
public partial class CreateOrganizationStrategy
{
    [CascadingParameter]
    public App App { get; set; }

    
    CreateOrganizationStrategyRequest Model { get; set; } = new();
    private void CancelAsync()
    {
        Navigation.NavigateBack();

    }

}
