using Server.Repositories;
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
                   var lastorder = await Repository.Context.Set<Scope>().Include(x => x.Deliverables)
                                    .Where(s => s.Id == Data.ScopeId)
                                    .SelectMany(s => s.Deliverables)
                                    .OrderByDescending(d => d.Order)
                                    .FirstOrDefaultAsync();
                    if (lastorder == null) return Result.Fail(Data.Fail);

                    var row = Deliverable.Create(Data.ScopeId, lastorder.Order + 1);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Deliverables.Cache.Key(row.Id)];

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
