using Shared.Models.SucessfullFactors.Requests;

namespace Server.EndPoint.SucessfullFactors.Commands
{
    public static class UpdateSucessfullFactorEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.SucessfullFactors.EndPoint.Update, async (UpdateSucessfullFactorRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<SucessfullFactor>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.SucessfullFactors.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static SucessfullFactor Map(this UpdateSucessfullFactorRequest request, SucessfullFactor row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
