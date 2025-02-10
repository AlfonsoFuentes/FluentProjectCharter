using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.Valves.Responses;

namespace Shared.Models.BudgetItems.Valves.Exports
{
    public record ValveGetAllExport(ExportFileType FileType, List<ValveResponse> query);
}
