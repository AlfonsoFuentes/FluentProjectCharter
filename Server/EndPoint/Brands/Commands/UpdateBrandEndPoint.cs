using Server.Database.Entities;
using Shared.Models.Brands.Requests;


namespace Server.EndPoint.Brands.Commands
{
    public static class UpdateBrandEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Brands.EndPoint.Update, async (UpdateBrandRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Brand>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [ .. StaticClass.Brands.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static Brand Map(this UpdateBrandRequest request, Brand row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
