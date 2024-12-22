using Shared.Models.HighLevelRequirements.Requests;


namespace Server.EndPoint.HighLevelRequirements.Commands
{
    public static class UpdateHighLevelRequirementEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.HighLevelRequirements.EndPoint.Update, async (UpdateHighLevelRequirementRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<HighLevelRequirement>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.HighLevelRequirements.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static HighLevelRequirement Map(this UpdateHighLevelRequirementRequest request, HighLevelRequirement row)
        {
            row.Name = request.Name;
   
            return row;
        }

    }

}
