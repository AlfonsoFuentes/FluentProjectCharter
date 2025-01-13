

namespace Server.EndPoint.OrganizationStrategys.Queries
{
    public static class GetOrganizationStrategyByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.OrganizationStrategys.EndPoint.GetById,
                    async (GetOrganizationStrategyByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<OrganizationStrategy>, IIncludableQueryable<OrganizationStrategy, object>> Includes = x =>
                    x.Include(x => x.Cases).ThenInclude(x => x.Project);
                    ;

                    Expression<Func<OrganizationStrategy, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.OrganizationStrategys.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria, Includes: Includes);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map();
                    return Result.Success(response);

                });
            }
        }


        public static OrganizationStrategyResponse Map(this OrganizationStrategy row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,

                Cases = row.Cases == null || row.Cases.Count == 0 ? new() : row.Cases.Select(x => x.MapShort()).ToList(),
            };
        }
       


    }
}
