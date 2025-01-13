using Shared.Models.Temporarys.Records;
using Shared.Models.Temporarys.Responses;

namespace Server.EndPoint.Temporarys.Queries
{
    public static class GetAllTemporaryEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Temporarys.EndPoint.GetAll, async (TemporaryGetAll request, IQueryRepository Repository) =>
                {
                   

                    string CacheKey = StaticClass.Temporarys.Cache.GetAll;
                    var rows = await Repository.GetAllAsync<Temporary>(CacheKey);

                    if (rows == null)
                    {
                        return Result<TemporaryResponseList>.Fail(
                        StaticClass.ResponseMessages.ReponseNotFound(StaticClass.Temporarys.ClassLegend));
                    }

                    var maps = rows.Select(x => x.Map()).ToList();


                    TemporaryResponseList response = new TemporaryResponseList()
                    {
                        Items = maps
                    };
                    return Result<TemporaryResponseList>.Success(response);

                });
            }
        }
    }
}