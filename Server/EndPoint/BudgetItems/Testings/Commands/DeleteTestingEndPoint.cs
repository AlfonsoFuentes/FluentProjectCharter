using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Testings.Requests;

namespace Server.EndPoint.Testings.Commands
{
    public static class DeleteTestingEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Testings.EndPoint.Delete, async (DeleteTestingRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Testing>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Testings.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
