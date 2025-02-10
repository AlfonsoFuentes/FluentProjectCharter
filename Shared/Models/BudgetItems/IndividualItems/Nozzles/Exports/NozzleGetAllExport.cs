using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;

namespace Shared.Models.BudgetItems.IndividualItems.Nozzles.Exports
{
    public record NozzleGetAllExport(ExportFileType FileType, List<NozzleResponse> query);
}
