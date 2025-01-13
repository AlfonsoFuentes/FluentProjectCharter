using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.EngineeringDesigns.Responses;

namespace Shared.Models.BudgetItems.EngineeringDesigns.Exports
{
    public record EngineeringDesignGetAllExport(ExportFileType FileType, List<EngineeringDesignResponse> query);
}
