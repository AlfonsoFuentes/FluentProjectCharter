﻿using Shared.Models.Requirements.Mappers;

namespace Server.EndPoint.Requirements.Commands
{
    public static class ChangeRequirementOrderUpEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Requirements.EndPoint.UpdateUp, async (ChangeRequirementOrderUpRequest Data, IRepository Repository) =>
                {


                    var row = await Repository.GetByIdAsync<Requirement>(Data.Id);

                    if (row == null) { return Result.Fail(Data.NotFound); }
                    if (row.Order == 1) { return Result.Success(Data.Succesfully); }

                    Expression<Func<Requirement, bool>> Criteria = x => x.ProjectId == Data.ProjectId && x.Order == row.Order - 1;

                    var previousRow = await Repository.GetAsync(Criteria: Criteria);

                    if (previousRow == null) { return Result.Fail(Data.NotFound); }

                    await Repository.UpdateAsync(previousRow);
                    await Repository.UpdateAsync(row);

                    row.Order = row.Order - 1;
                    previousRow.Order = row.Order + 1;

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            private string[] GetCacheKeys(Requirement row)
            {
                List<string> cacheKeys = [
                    .. StaticClass.Projects.Cache.Key(row.ProjectId),
           
                    .. StaticClass.Requirements.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }



    }

}
