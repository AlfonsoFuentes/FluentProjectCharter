using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.Instruments.Responses;

namespace Shared.Models.BudgetItems.Instruments.Exports
{
    public record InstrumentGetAllExport(ExportFileType FileType, List<InstrumentResponse> query);
}
