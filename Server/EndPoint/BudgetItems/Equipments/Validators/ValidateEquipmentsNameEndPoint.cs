using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Equipments.Validators;

namespace Server.EndPoint.Equipments.Validators
{
    public static class ValidateEquipmentsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Equipments.EndPoint.Validate, async (ValidateEquipmentRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Equipment, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Equipment, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Equipments.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
