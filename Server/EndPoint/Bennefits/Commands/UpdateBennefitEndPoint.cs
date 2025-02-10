using Server.Database.Entities;
using Shared.Models.Bennefits.Requests;


namespace Server.EndPoint.Bennefits.Commands
{
    public static class UpdateBennefitEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Bennefits.EndPoint.Update, async (UpdateBennefitRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Bennefit>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.Bennefits.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static Bennefit Map(this UpdateBennefitRequest request, Bennefit row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
