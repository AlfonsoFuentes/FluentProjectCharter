using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.EngineeringDesigns.Validators;

namespace Server.EndPoint.EngineeringDesigns.Validators
{
    public static class ValidateEngineeringDesignsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EngineeringDesigns.EndPoint.Validate, async (ValidateEngineeringDesignRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<EngineeringDesign, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<EngineeringDesign, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.EngineeringDesigns.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
