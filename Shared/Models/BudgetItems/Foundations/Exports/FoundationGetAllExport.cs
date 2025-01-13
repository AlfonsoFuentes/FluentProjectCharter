using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.Foundations.Responses;

namespace Shared.Models.BudgetItems.Foundations.Exports
{
    public record FoundationGetAllExport(ExportFileType FileType, List<FoundationResponse> query);
}
