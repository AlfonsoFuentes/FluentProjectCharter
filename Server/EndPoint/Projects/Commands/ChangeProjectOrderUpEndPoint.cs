using Shared.Models.Projects.Mappers;

namespace Server.EndPoint.Projects.Commands
{
    public static class ChangeProjectOrderUpEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.UpdateUp, async (ChangeProjectOrderUpRequest Data, IRepository Repository) =>
                {


                    var row = await Repository.GetByIdAsync<Project>(Data.Id);

                    if (row == null) { return Result.Fail(Data.NotFound); }
                    if (row.Order == 1) { return Result.Success(Data.Succesfully); }

                    Expression<Func<Project, bool>> Criteria = x => x.Order == row.Order - 1;

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
            private string[] GetCacheKeys(Project row)
            {
                List<string> cacheKeys = [
                    .. StaticClass.Projects.Cache.Key(row.Id),
           
               
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }



    }

}
