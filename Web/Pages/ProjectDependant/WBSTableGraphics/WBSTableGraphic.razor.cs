using Shared.Models.GanttTasks.Mappers;
using Shared.Models.GanttTasks.Records;
using Shared.Models.GanttTasks.Responses;
using Shared.Models.Projects.Reponses;

namespace Web.Pages.ProjectDependant.WBSTableGraphics;
public partial class WBSTableGraphic
{

    [Parameter]
    public ProjectResponse Project { get; set; } = null!;
    public DeliverableWithGanttTaskResponseList Response { get; set; } = new();
    protected override async Task OnParametersSetAsync()
    {
        await GetAll();
    }

    async Task GetAll()
    {

        var result = await GenericService.GetAll<DeliverableWithGanttTaskResponseListToUpdate, GanttTaskGetAll>(new GanttTaskGetAll
        {
            ProjectId = Project.Id,
        });

        if (result.Succeeded)
        {

            Response = result.Data.ToReponse();

            StateHasChanged();

        }

    }
}
