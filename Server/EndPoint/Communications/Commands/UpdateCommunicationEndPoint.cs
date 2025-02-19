using Server.Database.Entities.ProjectManagements;
using Shared.Models.Communications.Requests;


namespace Server.EndPoint.Communications.Commands
{
    public static class UpdateCommunicationEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Communications.EndPoint.Update, async (UpdateCommunicationRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Communication>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Communications.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Communication Map(this UpdateCommunicationRequest request, Communication row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
