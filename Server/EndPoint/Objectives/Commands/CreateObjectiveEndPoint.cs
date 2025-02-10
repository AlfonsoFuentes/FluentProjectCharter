using Shared.Models.Objectives.Requests;

namespace Server.EndPoint.Objectives.Commands
{

    public static class CreateObjectiveEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Objectives.EndPoint.Create, async (CreateObjectiveRequest Data, IRepository Repository) =>
                {


                    if (!Data.StartId.HasValue && !Data.PlanningId.HasValue)
                        return Result.Fail();
                    var lastorder = await Repository.GetLastOrderAsync<Objective, Project>(Data.ProjectId);

                    var row = Objective.Create(Data.ProjectId, Data.StartId, Data.PlanningId, lastorder);

                    await Repository.AddAsync(row);

                    row.Name = Data.Name;


                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            private string[] GetCacheKeys(Objective row)
            {
                List<string> cacheKeys = [
                    .. StaticClass.Projects.Cache.Key(row.ProjectId),
                    .. StaticClass.Objectives.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }

        }



        static Objective Map(this CreateObjectiveRequest request, Objective row)
        {
            row.Name = request.Name;

            return row;
        }

    }

}
