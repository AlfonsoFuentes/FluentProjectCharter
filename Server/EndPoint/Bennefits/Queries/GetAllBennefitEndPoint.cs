using Server.Database.Entities.ProjectManagements;

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

                    var maps = rows.OrderBy(x=>x.Order).Select(x=>x.Map()).ToList();

                    var response = new BennefitResponseList
                    {
                        Items = maps
                    };

                    return Result<BennefitResponseList>.Success(response);
                });
            }

            private static async Task<List<Bennefit>> GetBennefitAsync(BennefitGetAll request, IQueryRepository repository)
            {
         
                Expression<Func<Bennefit, bool>> criteria = x => x.ProjectId == request.ProjectId;
                string cacheKey = StaticClass.Bennefits.Cache.GetAll;

                return await repository.GetAllAsync(Cache: cacheKey, Criteria: criteria);
            }

           
        }
    }
}