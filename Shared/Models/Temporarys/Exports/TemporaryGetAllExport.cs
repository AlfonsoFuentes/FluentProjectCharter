using Shared.Enums.ExportFiles;
using Shared.Models.Temporarys.Responses;

namespace Shared.Models.Temporarys.Exports
{
    public record TemporaryGetAllExport(ExportFileType FileType, List<TemporaryResponse> query);
}
