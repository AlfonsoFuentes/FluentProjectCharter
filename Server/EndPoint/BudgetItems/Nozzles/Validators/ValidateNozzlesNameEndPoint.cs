using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Nozzles.Validators;

namespace Server.EndPoint.Nozzles.Validators
{
    public static class ValidateNozzlesNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Nozzles.EndPoint.Validate, async (ValidateNozzleRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Nozzle, bool>> CriteriaId = x => x.EngineeringItemId == Data.EngineeringItemId;
                    Func<Nozzle, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Nozzles.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
