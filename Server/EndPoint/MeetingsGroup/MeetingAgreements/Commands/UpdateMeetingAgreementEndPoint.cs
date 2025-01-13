using Shared.Models.MeetingAgreements.Requests;


namespace Server.EndPoint.MeetingsGroup.MeetingAgreements.Commands
{
    public static class UpdateMeetingAgreementEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.MeetingAgreements.EndPoint.Update, async (UpdateMeetingAgreementRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<MeetingAgreement>(Data.Id);
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


        static MeetingAgreement Map(this UpdateMeetingAgreementRequest request, MeetingAgreement row)
        {
            row.Name = request.Name;


            return row;
        }

    }
}
