using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.Pipes.Responses;

namespace Shared.Models.BudgetItems.Pipes.Exports
{
    public record PipeGetAllExport(ExportFileType FileType, List<PipeResponse> query);
}
