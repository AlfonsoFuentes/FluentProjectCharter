using Server.EndPoint.Projects.Queries;
using Shared.Models.ExpertJudgements.Records;

namespace Server.EndPoint.ExpertJudgements.Queries
{
    public static class GetExpertJudgementByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ExpertJudgements.EndPoint.GetById, async (GetExpertJudgementByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<ExpertJudgement>, IIncludableQueryable<ExpertJudgement, object>> Includes = x =>
                    x.Include(x => x.Expert!)
                    ;

                    Expression<Func<ExpertJudgement, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.ExpertJudgements.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria, Includes: Includes);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map(request.ProjectId);
                    return Result.Success(response);

                });
            }
        }



    }
}
