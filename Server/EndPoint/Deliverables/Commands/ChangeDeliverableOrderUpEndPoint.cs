using Server.Database.Entities.ProjectManagements;
using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{
    //public static class ChangeDeliverableOrderUpEndPoint
    //{
    //    public class EndPoint : IEndPoint
    //    {
    //        public void MapEndPoint(IEndpointRouteBuilder app)
    //        {
    //            app.MapPost(StaticClass.Deliverables.EndPoint.UpdateUp, async (UpdateDeliverableOrderUpRequest Data, IRepository Repository) =>
    //            {


    //                Expression<Func<Deliverable, bool>> Criteria = x => x.Id == Data.Id;
    //                Expression<Func<Deliverable, bool>> CriteriaPrevious = x => x.Id == Data.Prevoius.Id;

    //                var current = await Repository.GetAsync(Criteria: Criteria);
    //                var previous = await Repository.GetAsync(Criteria: CriteriaPrevious);
    //                if (current == null || previous == null) { return Result.Fail(Data.NotFound); }

    //                await Repository.UpdateAsync(previous);
    //                await Repository.UpdateAsync(current);

    //                current.Order = current.Order - 1;
    //                previous.Order = previous.Order + 1;

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
