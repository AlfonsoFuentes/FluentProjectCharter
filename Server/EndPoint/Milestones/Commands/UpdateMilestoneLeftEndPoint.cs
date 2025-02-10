using Shared.Models.Milestones.Requests;

namespace Server.EndPoint.Milestones.Commands
{
    public static class UpdateMilestoneLeftEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Milestones.EndPoint.UpdateLeft, async (UpdateMilestoneLeftRequest Data, IRepository Repository, IQueryRepository QueryRepository) =>
                {


                    var row = await Repository.GetByIdAsync<Milestone>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);

                    row.ParentMilestoneId = null;
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
