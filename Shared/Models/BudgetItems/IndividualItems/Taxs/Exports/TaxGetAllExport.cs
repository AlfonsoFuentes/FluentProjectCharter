using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.Taxs.Responses;

namespace Shared.Models.BudgetItems.Taxs.Exports
{
    public record TaxGetAllExport(ExportFileType FileType, List<TaxResponse> query);
}
