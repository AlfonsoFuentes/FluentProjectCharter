using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{
    public static class ChangeOrderDownEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.UpdateDown, async (ChangeDeliverableOrderDowmRequest Data, IRepository Repository) =>
                {
                    var lastorder = await Repository.Context.Set<Scope>().Include(x => x.Deliverables)
                                  .Where(s => s.Id == Data.ScopeId)
                                  .SelectMany(s => s.Deliverables)
                                  .OrderByDescending(d => d.Order)
                                  .FirstOrDefaultAsync();
                    if (lastorder == null) return Result.Fail(Data.Fail);
                    if (lastorder.Id == Data.Id) return Result.Success(Data.Succesfully);
                    var row = await Repository.GetByIdAsync<Deliverable>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }

                    Expression<Func<Deliverable, bool>> Criteria = x => x.ScopeId == Data.ScopeId && x.Order == row.Order + 1;

                    var nextRow = await Repository.GetAsync(Criteria: Criteria);

                    if (nextRow == null) { return Result.Fail(Data.NotFound); }

                    await Repository.UpdateAsync(nextRow);
                    await Repository.UpdateAsync(row);

                    nextRow.Order = nextRow.Order - 1;
                    row.Order = row.Order + 1;



                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Deliverables.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }



    }

}
