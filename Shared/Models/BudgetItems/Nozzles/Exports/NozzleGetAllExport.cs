using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.Nozzles.Responses;

namespace Shared.Models.BudgetItems.Nozzles.Exports
{
    public record NozzleGetAllExport(ExportFileType FileType, List<NozzleResponse> query);
}
