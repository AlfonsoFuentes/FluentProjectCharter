using Shared.Models.Requirements.Requests;


namespace Server.EndPoint.Requirements.Commands
{
    public static class UpdateRequirementEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Requirements.EndPoint.Update, async (UpdateRequirementRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Requirement>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.Requirements.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Requirement Map(this UpdateRequirementRequest request, Requirement row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
