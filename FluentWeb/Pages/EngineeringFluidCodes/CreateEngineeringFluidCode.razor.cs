using Microsoft.AspNetCore.Components;
using Shared.Models.OrganizationStrategies.Requests;
#nullable disable

namespace FluentWeb.Pages.EngineeringFluidCodes;
public partial class CreateEngineeringFluidCode
{
    [CascadingParameter]
    public App App { get; set; }

    
    CreateEngineeringFluidCodeRequest Model { get; set; } = new();
  

}
