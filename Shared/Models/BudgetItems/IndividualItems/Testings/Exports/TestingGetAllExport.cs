using Shared.Enums.ExportFiles;
using Shared.Models.BudgetItems.Testings.Responses;

namespace Shared.Models.BudgetItems.Testings.Exports
{
    public record TestingGetAllExport(ExportFileType FileType, List<TestingResponse> query);
}
