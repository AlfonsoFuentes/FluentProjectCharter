using Shared.Models.Projects.Mappers;

namespace Server.EndPoint.Projects.Commands
{
    public static class ChangeProjectOrderDownEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.UpdateDown, async (ChangeProjectOrderDowmRequest Data, IRepository Repository) =>
                {
                    var lastOrderEntity = await Repository.Context.Projects
                    .AsNoTracking()
                   .AsSplitQuery()
                   .AsQueryable()
                   .ToListAsync();
                    var lastorder = lastOrderEntity == null ? 1 : lastOrderEntity.MaxBy(x => x.Order)!.Order;


                    if (lastorder == Data.Order) return Result.Success(Data.Succesfully);


                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria: Criteria);
                    if (row == null) { return Result.Fail(Data.NotFound); }

                    Criteria = x => x.Order == row.Order + 1;

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
            private string[] GetCacheKeys(Project row, Guid ProjectId)
            {
                List<string> cacheKeys = [
                    .. StaticClass.Projects.Cache.Key(ProjectId),

                    .. StaticClass.Projects.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }



    }


}
