using DocumentFormat.OpenXml.Bibliography;

namespace Server.EndPoint.Projects.Queries
{
    public static class GetAllProjectEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.GetAll, async (ProjectGetAll request, IQueryRepository Repository) =>
                {
                    var apps = await Repository.GetAllAsync<App>(Cache: StaticClass.Apps.Cache.GetAll);
                    Guid? projectId = apps.Count == 1 ? apps[0].CurrentProjectId : null;

                    Expression<Func<Project, bool>>? criteria = projectId.HasValue ? (x => x.Id == projectId.Value) : null;

                    string CacheKey = StaticClass.Projects.Cache.GetAll;
                    var rows = await Repository.GetAllAsync<Project>(Cache: CacheKey, Criteria: criteria!, OrderBy: x => x.Order);
                    if (rows == null)
                    {
                        return Result<ProjectResponseList>.Fail(
                        StaticClass.ResponseMessages.ReponseNotFound(StaticClass.Projects.ClassLegend));
                    }
                    List<ProjectResponse> maps = rows.Count == 0 ? new() : rows.Select(x => x.Map()).ToList();

                    ProjectResponseList response = new ProjectResponseList()
                    {
                        SelectedProjectId = projectId,
                        Items = maps
                    };
                    return Result<ProjectResponseList>.Success(response);
                });
            }
        }
    }
}
