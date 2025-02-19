using Server.Database.Entities.ProjectManagements;
using Shared.Models.Backgrounds.Requests;


namespace Server.EndPoint.BackGrounds.Commands
{
    public static class UpdateBackGroundEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.BackGrounds.EndPoint.Update, async (UpdateBackGroundRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<BackGround>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [.. StaticClass.BackGrounds.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static BackGround Map(this UpdateBackGroundRequest request, BackGround row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
