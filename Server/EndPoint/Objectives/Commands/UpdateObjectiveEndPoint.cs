using Server.Database.Entities.ProjectManagements;
using Shared.Models.Objectives.Requests;


namespace Server.EndPoint.Objectives.Commands
{
    public static class UpdateObjectiveEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Objectives.EndPoint.Update, async (UpdateObjectiveRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Objective>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            private string[] GetCacheKeys(Objective row)
            {
                List<string> cacheKeys = [
           
                    .. StaticClass.Objectives.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static Objective Map(this UpdateObjectiveRequest request, Objective row)
        {
            row.Name = request.Name;
            //row.OrganizationStrategyId = request.OrganizationStrategy == null ? null : request.OrganizationStrategy.Id;
            return row;
        }

    }

}
