using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.Structurals.Responses;

namespace Shared.Models.BudgetItems.Structurals.Exports
{
    public record StructuralGetAllExport(ExportFileType FileType, List<StructuralResponse> query);
}
