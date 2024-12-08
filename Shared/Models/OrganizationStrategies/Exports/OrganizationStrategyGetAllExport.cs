using Shared.Enums.ExportFiles;
using Shared.Models.OrganizationStrategies.Responses;

namespace Shared.Models.OrganizationStrategies.Exports
{
    public record OrganizationStrategyGetAllExport(ExportFileType FileType, List<OrganizationStrategyResponse> query);
}
