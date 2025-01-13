namespace Server.EndPoint.Cases.Commands
{
    public static class UpdateCaseStateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Cases.EndPoint.UpdateState, async (CaseResponse Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Case>(Data.Id);
                    if (row == null) { return Result.Fail(); }
                    await Repository.UpdateAsync(row);

                    row.IsNodeOpen = Data.IsNodeOpen;
                    row.Tab = Data.Tab;

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Cases.Cache.Key(row.Id));


                    return Result.Success();

                });
            }
        }



    }

}
