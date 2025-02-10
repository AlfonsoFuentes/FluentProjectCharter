using Shared.Models.Qualitys.Requests;

namespace Server.EndPoint.Qualitys.Commands
{
    public static class DeleteQualityEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Qualitys.EndPoint.Delete, async (DeleteQualityRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Quality>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }


                    List<string> cache = [.. StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.Qualitys.Cache.Key(row.Id)];

                    await Repository.RemoveAsync(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
