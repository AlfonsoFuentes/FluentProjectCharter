using Server.Database.Entities.ProjectManagements;
using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{
    //public static class UpdateDeliverableEndPoint
    //{
    //    public class EndPoint : IEndPoint
    //    {
    //        public void MapEndPoint(IEndpointRouteBuilder app)
    //        {
    //            app.MapPost(StaticClass.Deliverables.EndPoint.Update, async (UpdateDeliverableRequest Data, IRepository Repository) =>
    //            {

    //                var row = await Repository.GetByIdAsync<Deliverable>(Data.Id);
    //                if (row == null) { return Result.Fail(Data.NotFound); }
    //                await Repository.UpdateAsync(row);
    //                Data.Map(row);
    //                var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

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


    //    static Deliverable Map(this UpdateDeliverableRequest request, Deliverable row)
    //    {
    //        row.Name = request.Name;
    //        return row;
    //    }

    //}

}
