using Shared.Models.HighLevelRequirements.Requests;

namespace Server.EndPoint.HighLevelRequirements.Commands
{
    public static class DeleteHighLevelRequirementEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.HighLevelRequirements.EndPoint.Delete, async (DeleteHighLevelRequirementRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<HighLevelRequirement>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);


                    List<string> cache = [..StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.HighLevelRequirements.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
