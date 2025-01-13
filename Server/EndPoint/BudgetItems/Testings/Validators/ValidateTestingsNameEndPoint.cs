using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Testings.Validators;

namespace Server.EndPoint.Testings.Validators
{
    public static class ValidateTestingsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Testings.EndPoint.Validate, async (ValidateTestingRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Testing, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Testing, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Testings.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
