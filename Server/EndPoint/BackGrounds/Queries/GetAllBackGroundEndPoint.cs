using Shared.Models.Backgrounds.Records;

namespace Server.EndPoint.BackGrounds.Queries
{
    public static class GetAllBackGroundEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.BackGrounds.EndPoint.GetAll, async (BackGroundGetAll request, IQueryRepository Repository) =>
                {

                    Expression<Func<BackGround, bool>> Criteria = x => x.ProjectId == request.ProjectId;
                    string CacheKey = StaticClass.BackGrounds.Cache.GetAll;
                    var rows = await Repository.GetAllAsync(Cache: CacheKey, Criteria: Criteria, OrderBy: x => x.Order);

                    if (rows == null)
                    {
                        return Result<BackGroundResponseList>.Fail(
                        StaticClass.ResponseMessages.ReponseNotFound(StaticClass.BackGrounds.ClassLegend));
                    }

                    var maps = rows.Select(x => x.Map()).ToList();


                    BackGroundResponseList response = new BackGroundResponseList()
                    {
                        Items = maps
                    };
                    return Result<BackGroundResponseList>.Success(response);

                });
            }
        }
    }
}