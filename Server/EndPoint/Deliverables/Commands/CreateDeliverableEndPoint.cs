using Server.Repositories;
using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{

    public static class CreateDeliverableEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.Create, async (CreateDeliverableRequest Data, IRepository Repository) =>
                {
                    if (!Data.PlanningId.HasValue && !Data.StartId.HasValue)
                        return Result.Fail();
                    var lastorder = await Repository.GetLastOrderAsync<Deliverable, Project>(Data.ProjectId);

                    var row = Deliverable.Create(Data.ProjectId, Data.StartId, Data.PlanningId, lastorder);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            private string[] GetCacheKeys(Deliverable row)
            {
                List<string> cacheKeys = [
                  
                    .. StaticClass.Projects.Cache.Key(row.ProjectId),
                    .. StaticClass.Deliverables.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static Deliverable Map(this CreateDeliverableRequest request, Deliverable row)
        {
            row.Name = request.Name;
          
            return row;
        }

    }



}
