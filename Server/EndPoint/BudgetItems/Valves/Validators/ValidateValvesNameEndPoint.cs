using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Valves.Validators;

namespace Server.EndPoint.Valves.Validators
{
    public static class ValidateValvesNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Valves.EndPoint.Validate, async (ValidateValveRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Valve, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Valve, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Valves.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
