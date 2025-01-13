using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.EngineeringDesigns.Requests;

namespace Server.EndPoint.EngineeringDesigns.Commands
{
    public static class DeleteEngineeringDesignEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EngineeringDesigns.EndPoint.Delete, async (DeleteEngineeringDesignRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<EngineeringDesign>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.EngineeringDesigns.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
