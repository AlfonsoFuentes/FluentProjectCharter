using Shared.Models.ExpertJudgements.Requests;

namespace Server.EndPoint.ExpertJudgements.Commands
{

    public static class CreateExpertJudgementEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ExpertJudgements.EndPoint.Create, async (CreateExpertJudgementRequest Data, IRepository Repository) =>
                {
                    var row = ExpertJudgement.Create(Data.CaseId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.ExpertJudgements.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static ExpertJudgement Map(this CreateExpertJudgementRequest request, ExpertJudgement row)
        {
            row.Name = request.Name;
            row.ExpertId = request.Expert == null ? null : request.Expert.Id;
            return row;
        }

    }

}
