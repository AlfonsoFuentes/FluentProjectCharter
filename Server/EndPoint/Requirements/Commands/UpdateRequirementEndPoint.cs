using Server.Database.Entities.ProjectManagements;
using Shared.Models.Requirements.Requests;


namespace Server.EndPoint.Requirements.Commands
{
    public static class UpdateRequirementEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Requirements.EndPoint.Update, async (UpdateRequirementRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Requirement>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
            private string[] GetCacheKeys(Requirement row)
            {
                List<string> cacheKeys = [
                  
                    .. StaticClass.Requirements.Cache.Key(row.Id, row.ProjectId)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static Requirement Map(this UpdateRequirementRequest request, Requirement row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
