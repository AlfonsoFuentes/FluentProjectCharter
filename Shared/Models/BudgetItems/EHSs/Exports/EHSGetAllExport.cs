using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.EHSs.Responses;

namespace Shared.Models.BudgetItems.EHSs.Exports
{
    public record EHSGetAllExport(ExportFileType FileType, List<EHSResponse> query);
}
