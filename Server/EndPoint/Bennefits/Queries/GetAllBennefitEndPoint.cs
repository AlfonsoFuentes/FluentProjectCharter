namespace Server.EndPoint.Bennefits.Queries
{
    public static class GetAllBennefitEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Bennefits.EndPoint.GetAll, async (BennefitGetAll request, IQueryRepository repository) =>
                {
                    var rows = await GetBennefitAsync(request, repository);

                    if (rows == null)
                    {
                        return Result<BennefitResponseList>.Fail(
                            StaticClass.ResponseMessages.ReponseNotFound(StaticClass.Bennefits.ClassLegend));
                    }

                    var maps = FilterBennefit(request, rows);

                    var response = new BennefitResponseList
                    {
                        Items = maps
                    };

                    return Result<BennefitResponseList>.Success(response);
                });
            }

            private static async Task<Project?> GetBennefitAsync(BennefitGetAll request, IQueryRepository repository)
            {
                Func<IQueryable<Project>, IIncludableQueryable<Project, object>> includes = x => x.Include(p => p.Bennefits);
                Expression<Func<Project, bool>> criteria = x => x.Id == request.ProjectId;
                string cacheKey = StaticClass.Bennefits.Cache.GetAll;

                return await repository.GetAsync(Cache: cacheKey, Includes: includes, Criteria: criteria);
            }

            private static List<BennefitResponse> FilterBennefit(BennefitGetAll request, Project project)
            {
                return project.Bennefits.OrderBy(x => x.Order).Select(ac => ac.Map()).ToList();
            }
        }
    }
}