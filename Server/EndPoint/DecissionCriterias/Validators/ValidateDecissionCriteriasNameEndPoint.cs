using Shared.Models.DecissionCriterias.Validators;
using Shared.Models.Backgrounds.Validators;

namespace Server.EndPoint.DecissionCriterias.Validators
{
    public static class ValidateDecissionCriteriasNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.DecissionCriterias.EndPoint.Validate, async (ValidateDecissionCriteriaRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<DecissionCriteria, bool>> CriteriaId = x => x.CaseId == Data.CaseId;
                    Func<DecissionCriteria, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.DecissionCriterias.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
