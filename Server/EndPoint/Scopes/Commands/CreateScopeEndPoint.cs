using Server.Database.Entities.ProjectManagements;
using Shared.Models.Scopes.Requests;

namespace Server.EndPoint.Scopes.Commands
{

    public static class CreateScopeEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Scopes.EndPoint.Create, async (CreateScopeRequest Data, IRepository Repository) =>
                {
                  
                    var lastorder = await Repository.GetLastOrderAsync<Scope, Project>(Data.ProjectId);

                    var row = Scope.Create(Data.ProjectId,lastorder);

                    await Repository.AddAsync(row);

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


        static Scope Map(this CreateScopeRequest request, Scope row)
        {
            row.Name = request.Name;
  
            return row;
        }

    }

}
