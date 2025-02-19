using Server.Database.Entities.ProjectManagements;
using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{
    //public static class UpdateDeliverableLeftEndPoint
    //{
    //    public class EndPoint : IEndPoint
    //    {
    //        public void MapEndPoint(IEndpointRouteBuilder app)
    //        {
    //            app.MapPost(StaticClass.Deliverables.EndPoint.UpdateLeft, async (UpdateDeliverableLeftRequest Data, IRepository Repository) =>
    //            {
    //                Expression<Func<Deliverable, bool>> Criteria = x => x.Id == Data.Id;


    //                var current = await Repository.GetAsync(Criteria: Criteria);


    //                if (current == null) { return Result.Fail(Data.NotFound); }

    //                await Repository.UpdateAsync(current);

    //                current.ParentDeliverableId = Data.Parent == null ? null : Data.Parent.Id;

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
