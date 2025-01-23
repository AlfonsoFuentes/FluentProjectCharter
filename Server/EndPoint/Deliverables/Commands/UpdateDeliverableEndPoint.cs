using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{
    public static class UpdateDeliverableEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.Update, async (UpdateDeliverableRequest Data, IRepository Repository) =>
                {

                    var row = await Repository.GetByIdAsync<Deliverable>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Deliverables.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static Deliverable Map(this UpdateDeliverableRequest request, Deliverable row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
