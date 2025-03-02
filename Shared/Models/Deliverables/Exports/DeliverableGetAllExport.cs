using Shared.Enums.ExportFiles;
using Shared.Models.Deliverables.Responses;

namespace Shared.Models.Deliverables.Exports
{
    public record DeliverableGetAllExport(ExportFileType FileType, List<DeliverableResponse> query);
}
