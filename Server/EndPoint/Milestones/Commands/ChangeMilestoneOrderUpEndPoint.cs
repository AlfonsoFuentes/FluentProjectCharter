using Shared.Models.Milestones.Requests;

namespace Server.EndPoint.Milestones.Commands
{
    public static class ChangeMilestoneOrderUpEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Milestones.EndPoint.UpdateUp, async (ChangeMilestoneOrderUpRequest Data, IRepository Repository) =>
                {


                    var row = await Repository.GetByIdAsync<Milestone>(Data.Id);

                    if (row == null) { return Result.Fail(Data.NotFound); }
                    if (row.Order == 1) { return Result.Success(Data.Succesfully); }

                    Expression<Func<Milestone, bool>> Criteria = x => x.ProjectId == Data.ProjectId && x.Order == row.Order - 1;

                    var previousRow = await Repository.GetAsync(Criteria: Criteria);

                    if (previousRow == null) { return Result.Fail(Data.NotFound); }

                    await Repository.UpdateAsync(previousRow);
                    await Repository.UpdateAsync(row);

                    row.Order = row.Order - 1;
                    previousRow.Order = row.Order + 1;

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            private string[] GetCacheKeys(Milestone row)
            {
                List<string> cacheKeys = [
        
                    .. StaticClass.Projects.Cache.Key(row.ProjectId),
                    .. StaticClass.Milestones.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }



    }

}
