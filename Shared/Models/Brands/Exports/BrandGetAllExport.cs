using Shared.Enums.ExportFiles;
using Shared.Models.Brands.Reponses;

namespace Shared.Models.Brands.Exports
{
    public record BrandGetAllExport(ExportFileType FileType, List<BrandResponse> query);
}
