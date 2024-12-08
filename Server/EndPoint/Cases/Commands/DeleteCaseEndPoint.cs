using Shared.Models.Cases.Requests;

namespace Server.EndPoint.Cases.Commands
{
    public static class DeleteCaseEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Cases.EndPoint.Delete, async (DeleteCaseRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Case>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);


                    List<string> cache = [..StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.Cases.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
