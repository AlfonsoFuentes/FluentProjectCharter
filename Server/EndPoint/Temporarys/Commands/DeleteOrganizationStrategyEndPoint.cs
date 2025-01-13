



using Server.Database.Entities;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.Temporarys.Requests;

namespace Server.EndPoint.Temporarys.Commands
{
    public static class DeleteTemporaryEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Temporarys.EndPoint.Delete, async (DeleteTemporaryRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Temporary>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Temporarys.Cache.Key(row.Id));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
