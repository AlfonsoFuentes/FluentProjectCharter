using Shared.Enums.ExportFiles;
using Shared.Models.DeliverableRisks.Responses;

namespace Shared.Models.DeliverableRisks.Exports
{
    public record DeliverableRiskGetAllExport(ExportFileType FileType, List<DeliverableRiskResponse> query);
}
