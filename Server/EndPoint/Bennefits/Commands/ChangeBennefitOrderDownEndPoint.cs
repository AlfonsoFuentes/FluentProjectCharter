﻿using Server.Database.Entities.ProjectManagements;
using Shared.Models.Bennefits.Mappers;

namespace Server.EndPoint.Bennefits.Commands
{
    public static class ChangeBennefitOrderDownEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Bennefits.EndPoint.UpdateDown, async (ChangeBennefitOrderDowmRequest Data, IRepository Repository) =>
                {
                    var lastorder = await Repository.GetLastOrderAsync<Bennefit, Project>(Data.ProjectId);


                    if (lastorder == Data.Order) return Result.Success(Data.Succesfully);

                   
                    Expression<Func<Bennefit, bool>> Criteria = x => x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria: Criteria);
                    if (row == null) { return Result.Fail(Data.NotFound); }

                    Criteria = x => x.ProjectId == Data.ProjectId && x.Order == row.Order + 1;

                    var nextRow = await Repository.GetAsync(Criteria: Criteria);

                    if (nextRow == null) { return Result.Fail(Data.NotFound); }

                    await Repository.UpdateAsync(nextRow);
                    await Repository.UpdateAsync(row);

                    nextRow.Order = nextRow.Order - 1;
                    row.Order = row.Order + 1;



                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row, Data.ProjectId));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            private string[] GetCacheKeys(Bennefit row, Guid ProjectId)
            {
                List<string> cacheKeys = [
                  
               
                    .. StaticClass.Bennefits.Cache.Key(row.Id, row.ProjectId)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }



    }


}
