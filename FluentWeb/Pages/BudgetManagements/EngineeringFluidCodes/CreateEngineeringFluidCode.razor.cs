using Microsoft.AspNetCore.Components;
using Shared.Models.EngineeringFluidCodes.Requests;

#nullable disable

namespace FluentWeb.Pages.BudgetManagements.EngineeringFluidCodes;
public partial class CreateEngineeringFluidCode
{
    [CascadingParameter]
    public App App { get; set; }


    CreateEngineeringFluidCodeRequest Model { get; set; } = new();


}
