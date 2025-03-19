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
                  
                    

                    string CacheKey = StaticClass.Projects.Cache.GetAll;
                    var rows = await Repository.GetAllAsync<Project>(Cache: CacheKey,  OrderBy: x => x.Order);
                    if (rows == null)
                    {
                        return Result<ProjectResponseList>.Fail(
                        StaticClass.ResponseMessages.ReponseNotFound(StaticClass.Projects.ClassLegend));
                    }
                    List<ProjectResponse> maps = rows.Count == 0 ? new() : rows.Select(x => x.Map()).ToList();

                    ProjectResponseList response = new ProjectResponseList()
                    {
                        
                        Items = maps
                    };
                    return Result<ProjectResponseList>.Success(response);
                });
            }
        }
    }
}
