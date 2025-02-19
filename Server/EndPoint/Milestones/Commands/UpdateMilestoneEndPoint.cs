using Shared.Models.Milestones.Requests;

namespace Server.EndPoint.Milestones.Commands
{
    public static class UpdateMilestoneEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Milestones.EndPoint.Update, async (UpdateMilestoneRequest Data, IRepository Repository) =>
                {

                    var row = await Repository.GetByIdAsync<Milestone>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
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


        static Milestone Map(this UpdateMilestoneRequest request, Milestone row)
        {
            row.Name = request.Name;
            row.StartDate = request.StartDate;
            row.EndDate = request.EndDate;
            row.Duration = request.Duration;

            return row;
        }

    }

}
