using Shared.Models.StakeHolderInsideProjects.Mappers;

namespace Server.EndPoint.StakeHolderInsideProjects.Commands
{
    //public static class ChangeStakeHolderInsideProjectOrderDownEndPoint
    //{
    //    public class EndPoint : IEndPoint
    //    {
    //        public void MapEndPoint(IEndpointRouteBuilder app)
    //        {
    //            app.MapPost(StaticClass.StakeHolderInsideProjects.EndPoint.UpdateDown, async (ChangeStakeHolderInsideProjectOrderDowmRequest Data, IRepository Repository) =>
    //            {
    //                //var lastorder = await Repository.GetLastOrderAsync<StakeHolderInsideProject, Project>(Data.ProjectId);


    //                //if (lastorder == Data.Order) return Result.Success(Data.Succesfully);

                   
    //                //Expression<Func<StakeHolderInsideProject, bool>> Criteria = x => x.Id == Data.Id;

    //                //var row = await Repository.GetAsync(Criteria: Criteria);
    //                //if (row == null) { return Result.Fail(Data.NotFound); }

    //                //Criteria = x => x.ProjectId == Data.ProjectId && x.Order == row.Order + 1;

    //                //var nextRow = await Repository.GetAsync(Criteria: Criteria);

    //                //if (nextRow == null) { return Result.Fail(Data.NotFound); }

    //                //await Repository.UpdateAsync(nextRow);
    //                //await Repository.UpdateAsync(row);

    //                //nextRow.Order = nextRow.Order - 1;
    //                //row.Order = row.Order + 1;



    //                //var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row, Data.ProjectId));

    //                //return Result.EndPointResult(result,
    //                //    Data.Succesfully,
    //                //    Data.Fail);


    //            });
    //        }
    //        //private string[] GetCacheKeys(StakeHolderInsideProject row, Guid ProjectId)
    //        //{
    //        //    List<string> cacheKeys = [
    //        //        .. StaticClass.Projects.Cache.Key(ProjectId),
               
    //        //        .. StaticClass.StakeHolderInsideProjects.Cache.Key(row.Id)
    //        //    ];
    //        //    return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
    //        //}
    //    }



    //}


}
