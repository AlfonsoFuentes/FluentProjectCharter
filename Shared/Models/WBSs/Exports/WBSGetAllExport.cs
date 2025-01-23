using Shared.Enums.ExportFiles;
using Shared.Models.WBSs.Responses;

namespace Shared.Models.WBSs.Exports
{
    public record WBSGetAllExport(ExportFileType FileType, List<WBSResponse> query);
}
