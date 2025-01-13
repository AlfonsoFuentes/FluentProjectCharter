namespace Server.EndPoint.MeetingsGroup.Meetings.Commands
{
    public static class UpdateMeetingStateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Meetings.EndPoint.UpdateState, async (MeetingResponse Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Meeting>(Data.Id);
                    if (row == null) { return Result.Fail(); }
                    await Repository.UpdateAsync(row);

                    row.IsNodeOpen = Data.IsNodeOpen;
                    row.Tab = Data.Tab;

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Meetings.Cache.Key(row.Id));


                    return Result.Success();

                });
            }
        }



    }

}
