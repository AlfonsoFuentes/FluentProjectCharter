using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Alterations.Validators;

namespace Server.EndPoint.Alterations.Validators
{
    public static class ValidateAlterationsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Alterations.EndPoint.Validate, async (ValidateAlterationRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Alteration, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Alteration, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Alterations.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
