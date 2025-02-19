using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.Electricals.Requests;
#nullable disable
namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Electricals;
public partial class CreateElectrical
{
    [CascadingParameter]
    public App App { get; set; }
    CreateElectricalRequest Model = new();

    [Parameter]
    public Guid ProjectId { get; set; }
    protected override void OnInitialized()
    {

        Model.ProjectId = ProjectId;
    }

}
