using Microsoft.AspNetCore.Components;
using Shared.Models.Deliverables.Records;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Deliverables.Responses.NewResponses;
using static Shared.StaticClasses.StaticClass;

namespace FluentWeb.Pages.TimeLineManagements.GanttCharts;
public partial class GanttChartPage
{
    [Parameter]
    public Guid ProjectId { get; set; }
    DeliverableResponseList Response { get; set; } = new();
    public List<DeliverableResponse> Items => Response.OrderedItems.Count == 0 ? new() :
     Response.OrderedItems;
    async Task GetAll()
    {
        var result = await GenericService.GetAll<DeliverableResponseList, DeliverableGetAll>(new DeliverableGetAll
        {
            ProjectId = ProjectId,
        });

        if (result.Succeeded)
        {
            // Actualizar la lista Items
            Response = result.Data;


        }
    }
    protected override async Task OnInitializedAsync()
    {
        await GetAll();
    }
}
