using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{
    public static class DeleteDeliverableEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.Delete, async (DeleteDeliverableRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Deliverable>, IIncludableQueryable<Deliverable, object>>Includes = x => x
                    .Include(x => x.Requirements)
                    .Include(x => x.Assumptions)
                    .Include(x => x.Constraints)
                    .Include(x => x.BudgetItems);

                    Expression<Func<Deliverable, bool>> Criteria = x => x.Id == Data.Id;


                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);


                    if (row == null) { return Result.Fail(Data.NotFound); }

                    foreach (var rowitem in row.Requirements)
                    {
                        rowitem.DeliverableId = null;
                        await Repository.RemoveAsync(rowitem);
                    }
                    foreach (var rowitem in row.Assumptions)
                    {
                        rowitem.DeliverableId = null;
                        await Repository.RemoveAsync(rowitem);
                    }
                    foreach (var rowitem in row.Constraints)
                    {
                        rowitem.DeliverableId = null;
                        await Repository.RemoveAsync(rowitem);
                    }
                    foreach (var rowitem in row.BudgetItems)
                    {
                        rowitem.DeliverableId = null;
                        await Repository.RemoveAsync(rowitem);
                    }

                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Deliverables.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
