using Shared.Models.MeetingAgreements.Requests;

namespace Server.EndPoint.MeetingsGroup.MeetingAgreements.Commands
{
    public static class CreateMeetingAgreementEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.MeetingAgreements.EndPoint.Create, async (CreateMeetingAgreementRequest Data, IRepository Repository) =>
                {
                    var row = MeetingAgreement.Create(Data.MeetingId);

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


        static MeetingAgreement Map(this CreateMeetingAgreementRequest request, MeetingAgreement row)
        {
            row.Name = request.Name;
   

            return row;
        }

    }
}
