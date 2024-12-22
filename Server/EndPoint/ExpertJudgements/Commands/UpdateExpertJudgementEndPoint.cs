using Shared.Models.ExpertJudgements.Requests;


namespace Server.EndPoint.ExpertJudgements.Commands
{
    public static class UpdateExpertJudgementEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ExpertJudgements.EndPoint.Update, async (UpdateExpertJudgementRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<ExpertJudgement>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.ExpertJudgements.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static ExpertJudgement Map(this UpdateExpertJudgementRequest request, ExpertJudgement row)
        {
            row.Name = request.Name;
            row.ExpertId = request.Expert == null ? null : request.Expert.Id;
            return row;
        }

    }

}
