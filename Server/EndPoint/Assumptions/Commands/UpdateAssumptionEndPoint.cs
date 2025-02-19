using Server.Database.Entities.ProjectManagements;
using Shared.Models.Assumptions.Requests;


namespace Server.EndPoint.Assumptions.Commands
{
    public static class UpdateAssumptionEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Assumptions.EndPoint.Update, async (UpdateAssumptionRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Assumption>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
            private string[] GetCacheKeys(Assumption row)
            {
                List<string> cacheKeys = [            
                    .. StaticClass.Assumptions.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static Assumption Map(this UpdateAssumptionRequest request, Assumption row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
