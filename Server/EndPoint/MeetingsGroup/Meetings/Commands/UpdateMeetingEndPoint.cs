using Shared.Models.Meetings.Requests;


namespace Server.EndPoint.MeetingsGroup.Meetings.Commands
{
    public static class UpdateMeetingEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Meetings.EndPoint.Update, async (UpdateMeetingRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Meeting>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Meetings.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Meeting Map(this UpdateMeetingRequest request, Meeting row)
        {
            row.Name = request.Name;
            row.DateofMeeting = request.DateofMeeting;
            row.Subject = request.Subject;
            row.MeetingType = request.MeetingType.Name;
            return row;
        }

    }
}
