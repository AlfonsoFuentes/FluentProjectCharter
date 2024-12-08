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
                    var row = Scope.Create(Data.CaseId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Scopes.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Scope Map(this CreateScopeRequest request, Scope row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
