using Shared.Enums.ExportFiles;
using Shared.Models.OrganizationStrategies.Responses;

namespace Shared.Models.OrganizationStrategies.Exports
{
    public record EngineeringFluidCodeGetAllExport(ExportFileType FileType, List<EngineeringFluidCodeResponse> query);
}
