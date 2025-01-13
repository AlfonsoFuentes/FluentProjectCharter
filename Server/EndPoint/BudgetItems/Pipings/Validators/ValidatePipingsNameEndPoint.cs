using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Pipings.Validators;

namespace Server.EndPoint.Pipings.Validators
{
    public static class ValidatePipingsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipings.EndPoint.Validate, async (ValidatePipingRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Isometric, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Isometric, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Pipings.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
