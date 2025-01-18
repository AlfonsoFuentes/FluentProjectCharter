using Server.Database.Entities;
using Shared.Models.Assumptions.Requests;


namespace Server.EndPoint.Assumptions.Commands
{
    public static class UpdateAssumptionEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Assumptions.EndPoint.Update, async (UpdateAssumptionRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Assumption>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.Assumptions.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static Assumption Map(this UpdateAssumptionRequest request, Assumption row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
