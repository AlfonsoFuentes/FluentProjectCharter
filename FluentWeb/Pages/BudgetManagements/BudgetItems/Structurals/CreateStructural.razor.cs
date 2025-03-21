using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.Structurals.Requests;

#nullable disable
namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Structurals;
public partial class CreateStructural
{
    [CascadingParameter]
    public App App { get; set; }
    CreateStructuralRequest Model = new();

    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid? GanttTaskId { get; set; }
    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;
        Model.GanttTaskId = GanttTaskId;

    }

}
