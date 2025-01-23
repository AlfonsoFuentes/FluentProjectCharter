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
                    Func<IQueryable<Deliverable>, IIncludableQueryable<Deliverable, object>> Includes = x => x
                .Include(x => x.BudgetItems);

                    Expression<Func<Deliverable, bool>> Criteria = x => x.Id == Data.Id;


                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);


                    if (row == null) { return Result.Fail(Data.NotFound); }

                    foreach (var rowitem in row.BudgetItems)
                    {
                        rowitem.DeliverableId = null;
                        await Repository.RemoveAsync(rowitem);
                    }
                    var scopeid = row.ScopeId;
                    await Repository.RemoveAsync(row);

                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Deliverables.Cache.Key(row.Id)];

                    await ReorderDeliverable(row.Id, scopeid, Repository);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
            async Task ReorderDeliverable(Guid RowId, Guid ScopeId, IRepository Repository)
            {

                Expression<Func<Deliverable, bool>> Criteria = x => x.ScopeId == ScopeId && x.Id != RowId;
                Expression<Func<Deliverable, object>> OrderBy = x => x.Order;

                var Deliverable = await Repository.GetAllAsync(Criteria: Criteria, OrderBy: x => x.Order);

                int order = 1;
                foreach (var row in Deliverable)
                {
                    row.Order = order;
                    await Repository.UpdateAsync(row);
                    order++;

                }

            }



        }




    }

}
