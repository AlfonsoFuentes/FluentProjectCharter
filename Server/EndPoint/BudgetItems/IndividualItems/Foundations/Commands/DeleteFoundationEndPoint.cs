using Shared.Models.BudgetItems.IndividualItems.Foundations.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Foundations.Commands
{
    public static class DeleteFoundationEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Foundations.EndPoint.Delete, async (DeleteFoundationRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Foundation>(Data.Id);
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
