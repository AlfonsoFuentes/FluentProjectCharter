using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Enums.DeliverableStatuss;
using Shared.Models.Deliverables.Mappers;
using Shared.Models.Deliverables.Records;
using Shared.Models.Deliverables.Responses.NewResponses;

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
        var result = await GenericService.GetAll<DeliverableResponseListToUpdate, DeliverableGetAll>(new DeliverableGetAll
        {
            ProjectId = ProjectId,
        });

        if (result.Succeeded)
        {
            // Actualizar la lista Items
            Response = result.Data.ToResponse();


        }
    }
    protected override async Task OnInitializedAsync()
    {
        await GetAll();
    }
    private int WindowWidth { get; set; }
    private DotNetObjectReference<GanttChartPage> _dotNetRef = null!;
    private RenderFragment RenderTaskRow(DeliverableResponse task, int level) => builder =>
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "gantt-sidebar-item");
        builder.AddAttribute(2, "style", $"padding-left: {level * 20}px");

        if (task.OrderedSubDeliverables.Any())
        {
            builder.OpenElement(3, "span");
            builder.AddAttribute(4, "class", "expand-icon");
            builder.AddAttribute(5, "onclick", EventCallback.Factory.Create(this, () => ToggleTask(task)));
            builder.AddContent(6, task.IsExpanded ? "▼" : "►");
            builder.CloseElement();
        }

        builder.AddContent(7, task.Name);
        builder.CloseElement();

        if (task.IsExpanded)
        {
            foreach (var subtask in task.OrderedSubDeliverables)
            {
                builder.AddContent(8, RenderTaskRow(subtask, level + 1));
            }
        }
    };

    private List<(DeliverableResponse Task, int Level)> GetFlattenedTasks(IEnumerable<DeliverableResponse> tasks, int level = 0)
    {
        var result = new List<(DeliverableResponse, int)>();
        foreach (var task in tasks)
        {
            result.Add((task, level));
            if (task.IsExpanded && task.OrderedSubDeliverables.Any())
            {
                result.AddRange(GetFlattenedTasks(task.OrderedSubDeliverables, level + 1));
            }
        }
        return result;
    }

    private void ToggleTask(DeliverableResponse task)
    {
        task.IsExpanded = !task.IsExpanded;
        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && Items.Count > 0)
        {
            var dimensions = await _jsRuntime.InvokeAsync<Dictionary<string, int>>("getWindowDimensions");
            WindowWidth = dimensions["width"];
            _dotNetRef = DotNetObjectReference.Create(this);
            await _jsRuntime.InvokeVoidAsync("onWindowResize", _dotNetRef);

            // Asegúrate de que los elementos estén presentes antes de sincronizar el desplazamiento
            await _jsRuntime.InvokeVoidAsync("syncScroll");
            await InvokeAsync(async () =>
            {
                StateHasChanged();
                await Task.Delay(100); // Espera breve para asegurar que el DOM esté actualizado
                await _jsRuntime.InvokeVoidAsync("syncRowHeights");
            });
        }
    }

    [JSInvokable]
    public void UpdateWindowDimensions(Dictionary<string, int> dimensions)
    {
        WindowWidth = dimensions["width"];
        StateHasChanged();
    }

    public void Dispose()
    {
        _dotNetRef?.Dispose();
    }

    private int ColumnWidth => Math.Max(60, (int)(WindowWidth / Response.Duration.Days));

    private string GetDeliverableColor(DeliverableStatus status)
    {
        return status switch
        {
            DeliverableStatus.Completed => "#4CAF50",    // Verde
            DeliverableStatus.InProgress => "#2196F3",   // Azul
            DeliverableStatus.Delayed => "#f44336",      // Rojo
            DeliverableStatus.OnHold => "#FFA726",       // Naranja
            _ => "#9E9E9E"                               // Gris para otros estados
        };
    }
}
