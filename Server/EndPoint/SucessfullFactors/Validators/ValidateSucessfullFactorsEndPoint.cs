using Shared.Models.SucessfullFactors.Validators;
using Shared.Models.Backgrounds.Validators;

namespace Server.EndPoint.SucessfullFactors.Validators
{
    public static class ValidateSucessfullFactorsEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.SucessfullFactors.EndPoint.Validate, async (ValidateSucessfullFactorRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<SucessfullFactor, bool>> CriteriaId = x => x.CaseId == Data.CaseId;
                    Func<SucessfullFactor, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.SucessfullFactors.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
