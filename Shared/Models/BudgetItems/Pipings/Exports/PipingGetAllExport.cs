using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.Pipings.Responses;

namespace Shared.Models.BudgetItems.Pipings.Exports
{
    public record PipingGetAllExport(ExportFileType FileType, List<PipingResponse> query);
}
