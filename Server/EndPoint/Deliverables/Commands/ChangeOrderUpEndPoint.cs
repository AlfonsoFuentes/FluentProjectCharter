using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{
    public static class ChangeOrderUpEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.UpdateUp, async (ChangeDeliverableOrderUpRequest Data, IRepository Repository) =>
                {


                    var row = await Repository.GetByIdAsync<Deliverable>(Data.Id);

                    if (row == null) { return Result.Fail(Data.NotFound); }
                    if (row.Order == 1) { return Result.Success(Data.Succesfully); }

                    Expression<Func<Deliverable, bool>> Criteria = x => x.ScopeId == Data.ScopeId && x.Order == row.Order - 1;

                    var previousRow = await Repository.GetAsync(Criteria: Criteria);

                    if (previousRow == null) { return Result.Fail(Data.NotFound); }

                    await Repository.UpdateAsync(previousRow);
                    await Repository.UpdateAsync(row);

                    row.Order = row.Order - 1;
                    previousRow.Order = row.Order + 1;

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
