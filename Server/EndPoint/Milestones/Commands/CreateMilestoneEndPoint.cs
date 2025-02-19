using Server.Repositories;
using Shared.Models.Milestones.Requests;

namespace Server.EndPoint.Milestones.Commands
{

    public static class CreateMilestoneEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Milestones.EndPoint.Create, async (CreateMilestoneRequest Data, IRepository Repository) =>
                {
                    if (!Data.PlanningId.HasValue && !Data.StartId.HasValue)
                        return Result.Fail();
                    var lastorder = await Repository.GetLastOrderAsync<Milestone, Project>(Data.ProjectId);

                    var row = Milestone.Create(Data.ProjectId, Data.StartId, Data.PlanningId, lastorder);

                    await Repository.AddAsync(row);

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


        static Milestone Map(this CreateMilestoneRequest request, Milestone row)
        {
            row.Name = request.Name;
           
            row.StartDate = request.StartDate;
            row.EndDate = request.EndDate;
            row.Duration = request.Duration;
       
            return row;
        }

    }



}
