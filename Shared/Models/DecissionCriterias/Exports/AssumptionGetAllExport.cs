using Shared.Enums.ExportFiles;
using Shared.Models.DecissionCriterias.Responses;

namespace Shared.Models.DecissionCriterias.Exports
{
    public record DecissionCriteriaGetAllExport(ExportFileType FileType, List<DecissionCriteriaResponse> query);
}
