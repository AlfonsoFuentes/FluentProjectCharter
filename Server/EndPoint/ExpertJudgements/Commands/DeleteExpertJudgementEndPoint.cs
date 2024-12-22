



using Server.Database.Entities;
using Shared.Models.ExpertJudgements.Requests;

namespace Server.EndPoint.ExpertJudgements.Commands
{
    public static class DeleteExpertJudgementEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ExpertJudgements.EndPoint.Delete, async (DeleteExpertJudgementRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<ExpertJudgement>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.ExpertJudgements.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
