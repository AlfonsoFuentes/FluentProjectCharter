using Server.Database.Entities.ProjectManagements;
using Shared.Models.StakeHolders.Requests;

namespace Server.EndPoint.StakeHolders.Commands
{
    public static class UpdateStakeHolderEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.StakeHolders.EndPoint.Update, async (UpdateStakeHolderRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<StakeHolder>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [.. StaticClass.StakeHolders.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static StakeHolder Map(this UpdateStakeHolderRequest request, StakeHolder row)
        {
            row.Name = request.Name;
            row.Email = request.Email;
            row.PhoneNumber = request.PhoneNumber;
            row.Area=request.Area;
            return row;
        }

    }

}
