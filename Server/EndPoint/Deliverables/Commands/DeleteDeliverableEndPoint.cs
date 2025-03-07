using Server.Database.Entities.ProjectManagements;
using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{
    public static class DeleteDeliverableEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.Delete, async (DeleteDeliverableRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Deliverable>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }


                    List<string> cache = [
                    ..StaticClass.Deliverables.Cache.Key(row.Id,row.ProjectId)];

                    await Repository.RemoveAsync(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
