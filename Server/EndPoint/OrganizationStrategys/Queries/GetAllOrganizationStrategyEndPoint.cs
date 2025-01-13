namespace Server.EndPoint.OrganizationStrategys.Queries
{
    public static class GetAllOrganizationStrategyEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.OrganizationStrategys.EndPoint.GetAll, async (OrganizationStrategyGetAll request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<OrganizationStrategy>, IIncludableQueryable<OrganizationStrategy, object>>
                     Includes = x => x.Include(x => x.Cases).ThenInclude(x => x.Project);

                    string CacheKey = StaticClass.OrganizationStrategys.Cache.GetAll;
                    var rows = await Repository.GetAllAsync(CacheKey, Includes: Includes);

                    if (rows == null)
                    {
                        return Result<OrganizationStrategyResponseList>.Fail(
                        StaticClass.ResponseMessages.ReponseNotFound(StaticClass.OrganizationStrategys.ClassLegend));
                    }

                    var maps = rows.Select(x => x.Map()).ToList();


                    OrganizationStrategyResponseList response = new OrganizationStrategyResponseList()
                    {
                        Items = maps
                    };
                    return Result<OrganizationStrategyResponseList>.Success(response);

                });
            }
        }
    }
}