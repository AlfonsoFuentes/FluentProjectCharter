



using Server.Database.Entities;
using Shared.Models.OrganizationStrategies.Requests;

namespace Server.EndPoint.OrganizationStrategys.Commands
{
    public static class DeleteOrganizationStrategyEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.OrganizationStrategys.EndPoint.Delete, async (DeleteOrganizationStrategyRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<OrganizationStrategy>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.OrganizationStrategys.Cache.Key(row.Id));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
