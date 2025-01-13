using Shared.Models.OrganizationStrategies.Exports;
using Shared.Models.OrganizationStrategies.Responses;
using Shared.Models.Temporarys.Exports;
using Shared.Models.Temporarys.Responses;

namespace Server.EndPoint.Temporarys.Exports
{
    public static class GetAllTemporaryExportEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Temporarys.EndPoint.Export, async (TemporaryGetAllExport request, IMediator Mediator) =>
                {
                    return await Mediator.Send(new Query(request));
                });
            }
        }
        record Query(TemporaryGetAllExport Data) : IRequest<IResult<FileResult>>
        {

        }
        class QueryHandler : IRequestHandler<Query, IResult<FileResult>>
        {
            private IQueryRepository Repository;
            private IAppDbContext _cache;

            public QueryHandler(IAppDbContext cache, IQueryRepository repository)
            {
                _cache = cache;
                Repository = repository;
            }

            public async Task<IResult<FileResult>> Handle(Query request, CancellationToken cancellationToken)
            {
                await Task.Delay(1);
                var maps = request.Data.query.Select(x => x.MapToResponseExcel()).AsQueryable();
                var response = request.Data.FileType == ExportFileType.pdf ?
                    ExportPDFExtension.ExportPDF(maps, "Temporarys", "Temporarys List") :
                    ExportExcelExtension.ToExcel(maps, "Temporarys", "List");


                return Result<FileResult>.Success(response);
            }
        }
        static TemporaryExportResponse MapToResponseExcel(this TemporaryResponse row)
        {
            return new()
            {
                Name = row.Name,

            };
        }


    }
}
