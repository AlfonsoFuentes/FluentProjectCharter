using Shared.Models.Milestones.Requests;

namespace Server.EndPoint.Milestones.Commands
{
    public static class ChangeMilestoneOrderDownEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Milestones.EndPoint.UpdateDown, async (ChangeMilestoneOrderDowmRequest Data, IRepository Repository) =>
                {
                    var lastorder = await Repository.Context.Set<Milestone>()
                                
                                  .Where(s => s.ProjectId == Data.ProjectId)
                                  
                                  .OrderByDescending(d => d.Order)
                                  .FirstOrDefaultAsync();
                    if (lastorder == null) return Result.Fail(Data.Fail);
                    if (lastorder.Id == Data.Id) return Result.Success(Data.Succesfully);
                    Func<IQueryable<Milestone>, IIncludableQueryable<Milestone, object>> Includes = x => x
                    .Include(x => x.Project);

                    Expression<Func<Milestone, bool>> Criteria = x => x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    if (row == null) { return Result.Fail(Data.NotFound); }

                    Criteria = x => x.ProjectId == Data.ProjectId && x.Order == row.Order + 1;

                    var nextRow = await Repository.GetAsync(Criteria: Criteria);

                    if (nextRow == null) { return Result.Fail(Data.NotFound); }

                    await Repository.UpdateAsync(nextRow);
                    await Repository.UpdateAsync(row);

                    nextRow.Order = nextRow.Order - 1;
                    row.Order = row.Order + 1;



                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            private string[] GetCacheKeys(Milestone row)
            {
                List<string> cacheKeys = [
         
                  
                    .. StaticClass.Milestones.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }



    }

}
