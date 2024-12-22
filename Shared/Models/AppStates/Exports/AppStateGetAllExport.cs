using Shared.Enums.ExportFiles;
using Shared.Models.AppStates.Responses;

namespace Shared.Models.AppStates.Exports
{
    public record AppStateGetAllExport(ExportFileType FileType, List<ActiveAppResponse> query);
}
