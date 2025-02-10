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

                    var cache = GetCacheKeys(row);
                  

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache);


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
            private string[] GetCacheKeys(Deliverable row)
            {
                List<string> cacheKeys = [
                    
                    .. StaticClass.Projects.Cache.Key(row.ProjectId),
                    .. StaticClass.Deliverables.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
            



        }




    }

}
