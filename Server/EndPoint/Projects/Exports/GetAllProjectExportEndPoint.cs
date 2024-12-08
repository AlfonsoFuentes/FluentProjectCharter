namespace Server.EndPoint.Projects.Exports
{
    public static class GetAllProjectExportEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.Export, async (ProjectGetAllExport request, IMediator Mediator) =>
                {
                    return await Mediator.Send(new Query(request));
                });
            }
        }
        record Query(ProjectGetAllExport Data) : IRequest<IResult<FileResult>>
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
                    ExportPDFExtension.ExportPDF(maps, "Projects", "Projects List") :
                    ExportExcelExtension.ToExcel(maps, "Projects", "List");


                return Result<FileResult>.Success(response);
            }
        }
        static ProjectExportResponse MapToResponseExcel(this ProjectResponse row)
        {
            return new()
            {
                Name = row.Name,

            };
        }


    }
}
