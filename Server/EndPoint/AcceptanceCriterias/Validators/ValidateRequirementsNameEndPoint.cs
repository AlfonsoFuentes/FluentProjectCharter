using Shared.Models.AcceptanceCriterias.Validators;
using Shared.Models.Backgrounds.Validators;

namespace Server.EndPoint.AcceptanceCriterias.Validators
{
    public static class ValidateAcceptanceCriteriasNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.AcceptanceCriterias.EndPoint.Validate, async (ValidateAcceptanceCriteriaRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<AcceptanceCriteria, bool>> CriteriaId = x => x.ScopeId == Data.ScopeId;
                    Func<AcceptanceCriteria, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.AcceptanceCriterias.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
