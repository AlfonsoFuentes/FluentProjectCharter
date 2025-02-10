using Shared.Models.BudgetItems.IndividualItems.Pipes.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Pipes.Commands
{
    public static class DeletePipeEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipes.EndPoint.Delete, async (DeletePipeRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Pipe>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    var cachekeys = GetCacheKeys(row);

                    await Repository.RemoveAsync(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cachekeys);

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });

            }
            private string[] GetCacheKeys(BudgetItem row)
            {
                List<string> cacheKeys = [
                ..StaticClass.Projects.Cache.Key(row.ProjectId),
                StaticClass.BudgetItems.Cache.GetAll

                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }




    }

}
