using Shared.Models.StakeHolders.Requests;

namespace Server.EndPoint.StakeHolders.Commands
{
    public static class CreateStakeHolderEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.StakeHolders.EndPoint.Create, async (CreateStakeHolderRequest Data, IRepository Repository) =>
                {
                   
                    var row = StakeHolder.Create();

                    await Repository.AddAsync(row);
                   
                    Data.Map(row);
                    List<string> cache = [ .. StaticClass.StakeHolders.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static StakeHolder Map(this CreateStakeHolderRequest request, StakeHolder row)
        {
            row.Name = request.Name;
            row.Email = request.Email;
            row.PhoneNumber = request.PhoneNumber;
            row.Area = request.Area;

            return row;
        }

    }

}
