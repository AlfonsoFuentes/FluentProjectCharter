using Shared.Models.Cases.Requests;


namespace Server.EndPoint.Cases.Commands
{
    public static class UpdateCaseEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Cases.EndPoint.Update, async (UpdateCaseRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Case>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.Cases.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static Case Map(this UpdateCaseRequest request, Case row)
        {
            row.Name = request.Name;
            row.OrganizationStrategyId = request.OrganizationStrategy == null ? null : request.OrganizationStrategy.Id;
            return row;
        }

    }

}
