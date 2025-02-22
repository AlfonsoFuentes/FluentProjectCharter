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
                    Func<IQueryable<Deliverable>, IIncludableQueryable<Deliverable, object>> Includes = x => null!;
        

                    Expression<Func<Deliverable, bool>> Criteria = x => x.Id == Data.Id;


                    var row = await Repository.GetAsync(Criteria: Criteria);


                    if (row == null) { return Result.Fail(Data.NotFound); }
                   
                    await Repository.RemoveAsync(row);

          

                    var cache = $"{StaticClass.Deliverables.Cache.GetAll(Data.ProjectId)}";
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache);


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
            



        }




    }

}
