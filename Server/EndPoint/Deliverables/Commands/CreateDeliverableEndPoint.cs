using Server.Database.Entities.ProjectManagements;
using Shared.Models.Deliverables.Requests;
using System.Threading;

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
                    
                    var lastorder = await Repository.GetLastOrderAsync<Deliverable, Project>(Data.ProjectId);

                    var row = Deliverable.Create(Data.ProjectId,lastorder);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Deliverables.Cache.Key(row.Id, row.ProjectId)];

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
