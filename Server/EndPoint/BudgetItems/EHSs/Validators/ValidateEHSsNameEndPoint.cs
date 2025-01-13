using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.EHSs.Validators;

namespace Server.EndPoint.EHSs.Validators
{
    public static class ValidateEHSsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EHSs.EndPoint.Validate, async (ValidateEHSRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<EHS, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<EHS, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.EHSs.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
