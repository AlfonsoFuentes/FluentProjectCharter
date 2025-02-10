using Shared.Models.BudgetItems;

namespace Server.EndPoint.BudgetItems.Commands
{
    //public static class MoveBudgetItemGroupEndPoint
    //{
    //    public class EndPoint : IEndPoint
    //    {
    //        public void MapEndPoint(IEndpointRouteBuilder app)
    //        {
    //            app.MapPost(StaticClass.BudgetItems.EndPoint.MoveGroup, async (MoveBudgetItemGroupRequest Data, IRepository Repository) =>
    //            {

    //                List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId)
    //                ];

    //                foreach (var datarow in Data.MoveGroup)
    //                {
    //                    var row = await Repository.GetByIdAsync<BudgetItem>(datarow);
    //                    if (row != null)
    //                    {
    //                        row.MilestoneId = Data.MilestoneId;
    //                        await Repository.UpdateAsync(row);

    //                    }


    //                }



    //                var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
    //                return Result.EndPointResult(result,
    //                    Data.Succesfully,
    //                    Data.Fail);

    //            });
    //        }
    //    }




    //}
    //public static class CopyBudgetItemGroupEndPoint
    //{
    //    public class EndPoint : IEndPoint
    //    {
    //        public void MapEndPoint(IEndpointRouteBuilder app)
    //        {
    //            app.MapPost(StaticClass.BudgetItems.EndPoint.CopyGroup, async (CopyBudgetItemGroupRequest Data, IRepository Repository) =>
    //            {

    //                List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId)
    //                ];

    //                foreach (var datarow in Data.CopyGroup)
    //                {
    //                    var row = await Repository.GetByIdAsync<BudgetItem>(datarow);
    //                    if (row != null)
    //                    {
    //                        row.MilestoneId = Data.MilestoneId;
    //                        await Repository.UpdateAsync(row);

    //                    }


    //                }



    //                var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
    //                return Result.EndPointResult(result,
    //                    Data.Succesfully,
    //                    Data.Fail);

    //            });
    //        }
    //    }




    //}

}
