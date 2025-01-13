using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Foundations.Validators;

namespace Server.EndPoint.Foundations.Validators
{
    public static class ValidateFoundationsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Foundations.EndPoint.Validate, async (ValidateFoundationRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Foundation, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Foundation, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Foundations.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
