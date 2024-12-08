using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{

    public static class CreateDeliverableEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.Create, async (CreateDeliverableRequest Data, IRepository Repository) =>
                {
                    var row = Deliverable.Create(Data.ScopeId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Deliverables.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Deliverable Map(this CreateDeliverableRequest request, Deliverable row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
