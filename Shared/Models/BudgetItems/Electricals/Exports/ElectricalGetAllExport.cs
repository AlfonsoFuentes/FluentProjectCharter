using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.Electricals.Responses;

namespace Shared.Models.BudgetItems.Electricals.Exports
{
    public record ElectricalGetAllExport(ExportFileType FileType, List<ElectricalResponse> query);
}
