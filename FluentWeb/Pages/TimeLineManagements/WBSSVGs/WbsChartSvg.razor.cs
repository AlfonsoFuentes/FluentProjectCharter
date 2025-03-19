using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Models.GanttTasks.Mappers;
using Shared.Models.GanttTasks.Records;
using Shared.Models.GanttTasks.Responses;

namespace FluentWeb.Pages.TimeLineManagements.WBSSVGs;
public partial class WbsChartSvg
{


    async Task GetAll()
    {

        var result = await GenericService.GetAll<DeliverableWithGanttTaskResponseListToUpdate, GanttTaskGetAll>(new GanttTaskGetAll
        {
            ProjectId = ProjectId,
        });

        if (result.Succeeded)
        {

            Data = result.Data.ToReponse();



        }

    }
    protected override async Task OnParametersSetAsync()
    {
        if (ProjectId == Guid.Empty) return;
        await GetAll();
    }
    [Parameter]
    public Guid ProjectId { get; set; }


    public DeliverableWithGanttTaskResponseList Data { get; set; } = null!;

    private int paddingX = 80; // Espaciado horizontal entre deliverables
    private int boxWidth = 200; // Ancho fijo del rectángulo
    private int lineHeight = 20; // Altura de cada línea de texto
    private int screenWidth = 0; // Ancho de la pantalla
    private DotNetObjectReference<WbsChartSvg> dotNetHelper = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Crear una referencia .NET para manejar eventos desde JavaScript
            dotNetHelper = DotNetObjectReference.Create(this);

            // Obtener el ancho inicial de la pantalla
            screenWidth = await JSRuntime.InvokeAsync<int>("getScreenWidth") - 200;

            // Configurar el listener de cambio de tamaño
            await JSRuntime.InvokeVoidAsync("setupResizeListener", dotNetHelper);

            StateHasChanged(); // Forzar actualización del componente
        }
    }

    [JSInvokable]
    public void UpdateScreenWidth(int newWidth)
    {
        screenWidth = newWidth - 100;
        StateHasChanged(); // Forzar actualización del componente
    }

    private List<int> GetXPositions()
    {
        var positions = new List<int>();
        int currentX = 10; // Posición inicial muy cercana al borde izquierdo

        foreach (var deliverable in Data.Deliverables)
        {
            positions.Add(currentX);
            currentX += boxWidth + paddingX; // Incrementar la posición horizontal
        }

        return positions;
    }

    private int GetSvgHeight()
    {
        int totalHeight = 50; // Margen superior inicial

        foreach (var deliverable in Data.Deliverables)
        {
            totalHeight = Math.Max(totalHeight, GetNodeHeight(deliverable) + 10); // Tomar la altura máxima
        }

        return totalHeight + 100;
    }

    private int GetNodeHeight(DeliverableWithGanttTaskResponse node)
    {
        int maxHeight = Math.Max(60, node.TextLines(boxWidth).Count * lineHeight + 10);

        if (node.OrderedItems != null && node.OrderedItems.Count > 0)
        {
            foreach (var item in node.OrderedItems)
            {
                maxHeight += GetSubtaskHeight(item) + 10;
            }
        }

        return maxHeight;
    }

    private int GetSubtaskHeight(GanttTaskResponse subtask)
    {
        int maxHeight = Math.Max(60, subtask.TextLines(boxWidth).Count * lineHeight + 10);

        if (subtask.OrderedSubGanttTasks != null && subtask.OrderedSubGanttTasks.Count > 0)
        {
            foreach (var child in subtask.OrderedSubGanttTasks)
            {
                maxHeight += GetSubtaskHeight(child) + 10;
            }
        }

        return maxHeight;
    }

    public void Dispose()
    {
        // Limpiar el listener de cambio de tamaño cuando el componente se destruye
        dotNetHelper?.Dispose();
    }
}
