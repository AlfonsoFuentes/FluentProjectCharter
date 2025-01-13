namespace Server.EndPoint.Deliverables.Commands
{
    public static class UpdateDeliverableStateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.UpdateState, async (DeliverableResponse Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Deliverable>(Data.Id);
                    if (row == null) { return Result.Fail(); }
                    await Repository.UpdateAsync(row);

                    row.IsNodeOpen = Data.IsNodeOpen;
                    row.Tab = Data.Tab;

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Deliverables.Cache.Key(row.Id));


                    return Result.Success();

                });
            }
        }



    }

}
