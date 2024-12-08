using Shared.Models.OrganizationStrategies.Exports;
using Shared.Models.OrganizationStrategies.Responses;

namespace Server.EndPoint.OrganizationStrategys.Exports
{
    public static class GetAllOrganizationStrategyExportEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.OrganizationStrategys.EndPoint.Export, async (OrganizationStrategyGetAllExport request, IMediator Mediator) =>
                {
                    return await Mediator.Send(new Query(request));
                });
            }
        }
        record Query(OrganizationStrategyGetAllExport Data) : IRequest<IResult<FileResult>>
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
                    ExportPDFExtension.ExportPDF(maps, "OrganizationStrategys", "OrganizationStrategys List") :
                    ExportExcelExtension.ToExcel(maps, "OrganizationStrategys", "List");


                return Result<FileResult>.Success(response);
            }
        }
        static OrganizationStrategyExportResponse MapToResponseExcel(this OrganizationStrategyResponse row)
        {
            return new()
            {
                Name = row.Name,

            };
        }


    }
}
