using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Structurals.Validators;

namespace Server.EndPoint.Structurals.Validators
{
    public static class ValidateStructuralsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Structurals.EndPoint.Validate, async (ValidateStructuralRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Structural, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Structural, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Structurals.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
