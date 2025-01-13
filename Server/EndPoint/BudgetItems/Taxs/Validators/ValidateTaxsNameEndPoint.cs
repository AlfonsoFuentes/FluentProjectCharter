using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Taxs.Validators;

namespace Server.EndPoint.Taxs.Validators
{
    public static class ValidateTaxsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Taxs.EndPoint.Validate, async (ValidateTaxRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Tax, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Tax, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Taxs.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
