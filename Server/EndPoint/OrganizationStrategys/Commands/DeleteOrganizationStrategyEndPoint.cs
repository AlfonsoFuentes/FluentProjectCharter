



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
                    Func<IQueryable<OrganizationStrategy>, IIncludableQueryable<OrganizationStrategy, object>>
                     Includes = x => x.Include(x => x.Cases);

                    Expression<Func<OrganizationStrategy, bool>> Criteria = x => x.Id == Data.Id;


                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);


                    if (row == null) { return Result.Fail(Data.NotFound); }

                    foreach (var cases in row.Cases)
                    {
                        cases.OrganizationStrategyId = null;
                        await Repository.UpdateAsync(cases);
                    }

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
