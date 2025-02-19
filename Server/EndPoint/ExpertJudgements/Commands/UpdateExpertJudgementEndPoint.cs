using Server.Database.Entities.ProjectManagements;
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
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            private string[] GetCacheKeys(ExpertJudgement row)
            {
                List<string> cacheKeys = [
                    .. StaticClass.ExpertJudgements.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
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
