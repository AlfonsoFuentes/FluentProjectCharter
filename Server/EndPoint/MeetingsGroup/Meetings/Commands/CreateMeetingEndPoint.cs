using Shared.Models.Meetings.Requests;

namespace Server.EndPoint.MeetingsGroup.Meetings.Commands
{

    public static class CreateMeetingEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Meetings.EndPoint.Create, async (CreateMeetingRequest Data, IRepository Repository) =>
                {
                    var row = Meeting.Create(Data.ProjectId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Meetings.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Meeting Map(this CreateMeetingRequest request, Meeting row)
        {
            row.Name = request.Name;
            row.DateofMeeting = request.DateofMeeting;
            row.Subject = request.Subject;
            row.MeetingType = request.MeetingType.Name;
            return row;
        }

    }
}
