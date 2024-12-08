using Shared.Enums.ExportFiles;
using Shared.Models.SucessfullFactors.Responses;

namespace Shared.Models.SucessfullFactors.Exports
{
    public record SucessfullFactorGetAllExport(ExportFileType FileType, List<SucessfullFactorResponse> query);
}
