using Shared.Models.BudgetItems.Equipments.Validators;

namespace Server.EndPoint.Equipments.Validators
{
    public static class ValidateEquipmentTagsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Equipments.EndPoint.ValidateTag, async (ValidateEquipmentTagRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Equipment, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Equipment, bool> CriteriaExist = x => Data.Id == null ?
                    x.Tag.Equals(Data.Tag) : x.Id != Data.Id.Value && x.Tag.Equals(Data.Tag);
                    string CacheKey = StaticClass.Equipments.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
