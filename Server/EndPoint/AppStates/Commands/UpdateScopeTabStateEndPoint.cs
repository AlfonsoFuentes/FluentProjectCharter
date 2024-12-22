using Shared.Models.AppStates.Requests;


namespace Server.EndPoint.AppStates.Commands
{
    public static partial class UpdateScopeTabStateEndPoint
    {
        public class EndPointProject : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.AppStates.EndPoint.UpdateScopeTab, async (UpdateActiveStateRequest Data, IRepository Repository) =>
                {
                    Expression<Func<Scope, bool>> Criteria = x => x.Id ==
                   Data.ScopeId;

                    var row = await Repository.GetAsync(Criteria: Criteria);
                    if (row == null)
                    {
                        return Result.Fail();
                    }
                    await Repository.UpdateAsync(row);
                    row.ScopeTab = Data.ScopeTab;

                    List<string> cache = [.. StaticClass.AppStates.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());


                    return Result.EndPointResult(result,
                        "",
                        "");



                });
            }
        }

    }

}
