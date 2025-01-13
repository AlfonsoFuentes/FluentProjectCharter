using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.Equipments.Responses;

namespace Shared.Models.BudgetItems.Equipments.Exports
{
    public record EquipmentGetAllExport(ExportFileType FileType, List<EquipmentResponse> query);
}
