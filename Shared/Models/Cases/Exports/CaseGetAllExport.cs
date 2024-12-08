using Shared.Enums.ExportFiles;
using Shared.Models.Cases.Responses;

namespace Shared.Models.Cases.Exports
{
    public record CaseGetAllExport(ExportFileType FileType, List<CaseResponse> query);
}
