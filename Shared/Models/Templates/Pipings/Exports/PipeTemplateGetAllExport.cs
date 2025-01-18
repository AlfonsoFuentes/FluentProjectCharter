using Shared.Enums.ExportFiles;
using Shared.Models.Templates.Pipes.Responses;

namespace Shared.Models.Templates.Pipes.Exports
{
    public record PipeTemplateGetAllExport(ExportFileType FileType, List<PipeTemplateResponse> query);
}
