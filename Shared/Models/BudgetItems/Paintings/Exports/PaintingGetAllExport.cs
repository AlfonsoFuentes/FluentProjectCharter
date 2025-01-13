using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.Paintings.Responses;

namespace Shared.Models.BudgetItems.Paintings.Exports
{
    public record PaintingGetAllExport(ExportFileType FileType, List<PaintingResponse> query);
}
