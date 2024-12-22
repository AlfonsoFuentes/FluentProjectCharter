using Shared.Enums.ExportFiles;
using Shared.Models.HighLevelRequirements.Responses;

namespace Shared.Models.HighLevelRequirements.Exports
{
    public record HighLevelRequirementGetAllExport(ExportFileType FileType, List<HighLevelRequirementResponse> query);
}
