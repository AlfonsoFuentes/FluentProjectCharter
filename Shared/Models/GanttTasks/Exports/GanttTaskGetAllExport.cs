using Shared.Enums.ExportFiles;
using Shared.Models.GanttTasks.Responses;

namespace Shared.Models.GanttTasks.Exports
{
    public record GanttTaskGetAllExport(ExportFileType FileType, List<GanttTaskResponse> query);
}
