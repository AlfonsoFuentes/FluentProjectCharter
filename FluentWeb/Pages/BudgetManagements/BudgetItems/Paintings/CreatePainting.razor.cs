using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.Paintings.Requests;

#nullable disable
namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Paintings;
public partial class CreatePainting
{
    [CascadingParameter]
    public App App { get; set; }
    CreatePaintingRequest Model = new();

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
