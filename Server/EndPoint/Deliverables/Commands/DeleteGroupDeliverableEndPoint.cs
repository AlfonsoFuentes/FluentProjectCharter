using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{
    public static class DeleteGroupDeliverableEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.DeleteGroup, async (DeleteGroupDeliverableRequest Data, IRepository Repository) =>
                {
                    foreach (var rowItem in Data.SelecteItems)
                    {
                        var row = await Repository.GetByIdAsync<Deliverable>(rowItem.Id);
                        if (row != null)
                        {
                            await Repository.RemoveAsync(row);
                        }
                    }


                    var cache = StaticClass.Deliverables.Cache.GetAll(Data.ProjectId);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache);
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
