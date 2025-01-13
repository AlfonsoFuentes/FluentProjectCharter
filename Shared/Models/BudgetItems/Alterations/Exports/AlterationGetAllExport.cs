using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.Alterations.Responses;

namespace Shared.Models.BudgetItems.Alterations.Exports
{
    public record AlterationGetAllExport(ExportFileType FileType, List<AlterationResponse> query);
}
