using Microsoft.AspNetCore.Components;
using Shared.Models.GanttTasks.Responses;

namespace FluentWeb.Pages.TimeLineManagements.WBSSVGs;
public partial class SubtaskNode
{
    [CascadingParameter]
    public WbsChartSvg MainPage { get; set; } = null!;

    [Parameter]
    public GanttTaskResponse Node { get; set; } = null!;

    [Parameter]
    public int ParentX { get; set; }

    [Parameter]
    public int ParentY { get; set; }

    [Parameter]
    public int Level { get; set; } = 0;

    private int BoxWidth { get; set; } = 200;
    private int lineHeight = 20;
    private int boxHeight;

    protected override void OnParametersSet()
    {
        // Calcular la altura del nodo en funci�n del n�mero de l�neas de texto
        boxHeight = Math.Max(60, Node.TextLines(BoxWidth).Count * lineHeight + 10);
    }

    private string GenerateSvgText()
    {
        var svgText = new System.Text.StringBuilder();
        int paddingTop = 20; // Padding superior para el texto

        for (int i = 0; i < Node.TextLines(BoxWidth).Count; i++)
        {
            svgText.Append($"<text x=\"{ParentX + Level * 10 + 10}\" y=\"{ParentY + paddingTop + i * lineHeight}\" font-family=\"Arial\" font-size=\"14\" fill=\"#333\">");
            svgText.Append(Node.TextLines(BoxWidth)[i]);
            svgText.Append("</text>");
        }

        return svgText.ToString();
    }

    private int GetSubtaskHeight(GanttTaskResponse subtask)
    {
        int maxHeight = Math.Max(60, subtask.TextLines(BoxWidth).Count * lineHeight + 10);

        if (subtask.OrderedSubGanttTasks != null && subtask.OrderedSubGanttTasks.Count > 0)
        {
            foreach (var child in subtask.OrderedSubGanttTasks)
            {
                maxHeight += GetSubtaskHeight(child) + 10;
            }
        }

        return maxHeight;
    }

}
