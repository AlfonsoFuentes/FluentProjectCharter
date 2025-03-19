using Server.Database.Entities.ProjectManagements;
using Shared.Models.Qualitys.Requests;


namespace Server.EndPoint.Qualitys.Commands
{
    public static class UpdateQualityEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Qualitys.EndPoint.Update, async (UpdateQualityRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Quality>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Qualitys.Cache.Key(row.Id, row.ProjectId)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Quality Map(this UpdateQualityRequest request, Quality row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
