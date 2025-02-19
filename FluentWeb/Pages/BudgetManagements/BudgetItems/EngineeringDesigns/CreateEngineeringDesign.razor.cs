using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Requests;
using static Shared.StaticClasses.StaticClass;
#nullable disable
namespace FluentWeb.Pages.BudgetManagements.BudgetItems.EngineeringDesigns;
public partial class CreateEngineeringDesign
{
    [CascadingParameter]
    public App App { get; set; }
    CreateEngineeringDesignRequest Model = new();

    [Parameter]
    public Guid ProjectId { get; set; }
    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;

    }

}
