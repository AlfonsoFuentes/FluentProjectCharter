using Server.Database.Entities.ProjectManagements;
using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{
    public static class UpdateDeliverableExpandEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.UpdateExpand, async (UpdateDeliverableExpandRequest Data, IRepository Repository) =>
                {
                    var row =await  Repository.GetByIdAsync<Deliverable>(Data.Id);

                    if (row == null)
                        return Result.Fail();

                    await Repository.UpdateAsync(row);

                    row.IsExpanded=Data.Expanded;
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
