using Server.Database.Entities;
using Shared.Models.Milestones.Requests;

namespace Server.EndPoint.Milestones.Commands
{
    public static class DeleteMilestoneEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Milestones.EndPoint.Delete, async (DeleteMilestoneRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Milestone>, IIncludableQueryable<Milestone, object>> Includes = x => null!;
                //.Include(x => x.BudgetItems);

                    Expression<Func<Milestone, bool>> Criteria = x => x.Id == Data.Id;


                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);


                    if (row == null) { return Result.Fail(Data.NotFound); }
                    //foreach (var rowitem in row.BudgetItems)
                    //{
                    //    rowitem.MilestoneId = null;
                    //    await Repository.RemoveAsync(rowitem);
                    //}


                    await Repository.RemoveAsync(row);

                    var cache = GetCacheKeys(row);

                    //await ReorderMilestone(row.Id, row.ProjectId, Repository);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache);


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
            private string[] GetCacheKeys(Milestone row)
            {
                List<string> cacheKeys = [
               
                 
                    .. StaticClass.Milestones.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
            //async Task ReorderMilestone(Guid RowId, Guid Projects, IRepository Repository)
            //{

            //    Expression<Func<Milestone, bool>> Criteria = x => x.ProjectId == Projects && x.Id != RowId;


            //    var Milestone = await Repository.GetAllAsync(Criteria: Criteria, OrderBy: x => x.Order);

            //    int order = 1;
            //    foreach (var row in Milestone)
            //    {
            //        row.Order = order;
            //        await Repository.UpdateAsync(row);
            //        order++;

            //    }

            //}



        }




    }

}
