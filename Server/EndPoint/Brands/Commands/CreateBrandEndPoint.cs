


using Server.Database.Entities;
using Shared.Models.Brands.Requests;
using System.Threading;

namespace Server.EndPoint.Brands.Commands
{

    public static class CreateBrandEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Brands.EndPoint.Create, async (CreateBrandRequest Data, IRepository Repository) =>
                {
                    var row = Brand.Create();

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Brands.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Brand Map(this CreateBrandRequest request, Brand row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
