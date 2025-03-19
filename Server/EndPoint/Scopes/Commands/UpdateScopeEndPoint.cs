using Server.Database.Entities.ProjectManagements;
using Shared.Models.Scopes.Requests;

namespace Server.EndPoint.Scopes.Commands
{
    public static class UpdateScopeEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Scopes.EndPoint.Update, async (UpdateScopeRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Scope>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);
                });


            }


            private string[] GetCacheKeys(Scope row)
            {
                List<string> cacheKeys = [
                    
             
                    .. StaticClass.Scopes.Cache.Key(row.Id, row.ProjectId)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static Scope Map(this UpdateScopeRequest request, Scope row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
