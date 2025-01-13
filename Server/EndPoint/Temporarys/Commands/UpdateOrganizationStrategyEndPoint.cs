using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.Temporarys.Requests;

namespace Server.EndPoint.Temporarys.Commands
{
    public static class UpdateTemporaryEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Temporarys.EndPoint.Update, async (UpdateTemporaryRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Temporary>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Temporarys.Cache.Key(row.Id));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static Temporary Map(this UpdateTemporaryRequest request, Temporary row)
        {
            
            return row;
        }

    }

}
