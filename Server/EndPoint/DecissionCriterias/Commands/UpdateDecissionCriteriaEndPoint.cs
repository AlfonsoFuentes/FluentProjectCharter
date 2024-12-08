using Shared.Models.DecissionCriterias.Requests;


namespace Server.EndPoint.DecissionCriterias.Commands
{
    public static class UpdateDecissionCriteriaEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.DecissionCriterias.EndPoint.Update, async (UpdateDecissionCriteriaRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<DecissionCriteria>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.DecissionCriterias.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static DecissionCriteria Map(this UpdateDecissionCriteriaRequest request, DecissionCriteria row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
