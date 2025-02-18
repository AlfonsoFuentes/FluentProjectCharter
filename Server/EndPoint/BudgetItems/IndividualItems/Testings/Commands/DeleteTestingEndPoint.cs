﻿using Shared.Models.BudgetItems.Testings.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Testings.Commands
{
    public static class DeleteTestingEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Testings.EndPoint.Delete, async (DeleteTestingRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Testing>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    var cachekeys = GetCacheKeys(row);

                    await Repository.RemoveAsync(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cachekeys);

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });

            }
            private string[] GetCacheKeys(BudgetItem row)
            {
                List<string> cacheKeys = [
                 ..StaticClass.BudgetItems.Cache.Key(row.Id)

                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }




    }

}
