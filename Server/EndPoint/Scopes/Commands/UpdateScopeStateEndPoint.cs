namespace Server.EndPoint.Scopes.Commands
{
    public static class UpdateScopeStateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Scopes.EndPoint.UpdateState, async (ScopeResponse Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Scope>(Data.Id);
                    if (row == null) { return Result.Fail(); }
                    await Repository.UpdateAsync(row);

                    row.IsNodeOpen = Data.IsNodeOpen;
                    row.Tab = Data.Tab;

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Scopes.Cache.Key(row.Id));


                    return Result.Success();

                });
            }
        }



    }

}
