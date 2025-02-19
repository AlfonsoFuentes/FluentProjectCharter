using Server.Database.Entities.ProjectManagements;
using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{
    //public static class UpdateDeliverableOrderDownEndPoint
    //{
    //    public class EndPoint : IEndPoint
    //    {
    //        public void MapEndPoint(IEndpointRouteBuilder app)
    //        {
    //            app.MapPost(StaticClass.Deliverables.EndPoint.UpdateDown, async (UpdateDeliverableOrderDowmRequest Data, IRepository Repository) =>
    //            {
    //                Expression<Func<Deliverable, bool>> Criteria = x => x.Id == Data.Id;
    //                Expression<Func<Deliverable, bool>> CriteriaNext = x => x.Id == Data.Next.Id;

    //                var current = await Repository.GetAsync(Criteria: Criteria);
    //                var next = await Repository.GetAsync(Criteria: CriteriaNext);

    //                if (current == null || next == null) { return Result.Fail(Data.NotFound); }

                    
    //                await Repository.UpdateAsync(current);
    //                await Repository.UpdateAsync(next);

    //                next.Order = next.Order - 1;
    //                current.Order = current.Order + 1;



    //                var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(current));

    //                return Result.EndPointResult(result,
    //                    Data.Succesfully,
    //                    Data.Fail);


    //            });
    //        }
    //        private string[] GetCacheKeys(Deliverable row)
    //        {
    //            List<string> cacheKeys = [
    //                .. StaticClass.Deliverables.Cache.Key(row.Id)
    //            ];
    //            return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
    //        }
    //    }



    //}


}
