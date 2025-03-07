using Microsoft.AspNetCore.Components;
using Shared.Models.GanttTasks.Mappers;
using Shared.Models.GanttTasks.Responses;

namespace FluentWeb.Pages.TimeLineManagements.DeliverablesGanttTasks;
public partial class DeliverableRow
{
    [Parameter]
    public DeliverableWithGanttTaskResponse Deliverable { get; set; } = null!;
   
    public async Task OnToggleTask(DeliverableWithGanttTaskResponse row)
    {

        Deliverable.IsExpanded = !row.IsExpanded;


        var result = await GenericService.Update(row.ToExpand());
        if (result.Succeeded)
        {

        }
    }

}
