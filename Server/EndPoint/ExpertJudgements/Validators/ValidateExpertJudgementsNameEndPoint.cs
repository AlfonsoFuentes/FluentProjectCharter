using Shared.Models.ExpertJudgements.Validators;
using Shared.Models.Backgrounds.Validators;

namespace Server.EndPoint.ExpertJudgements.Validators
{
    public static class ValidateExpertJudgementsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ExpertJudgements.EndPoint.Validate, async (ValidateExpertJudgementRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<ExpertJudgement, bool>> CriteriaId = x => x.CaseId == Data.CaseId;
                    Func<ExpertJudgement, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.ExpertJudgements.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
