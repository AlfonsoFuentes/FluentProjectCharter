

using Server.Database.Entities;
using Shared.Models.HighLevelRequirements.Validators;

namespace Server.EndPoint.HighLevelRequirements.Validators
{
    public static class ValidateHighLevelRequirementNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.HighLevelRequirements.EndPoint.Validate, async (ValidateHighLevelRequirementRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<HighLevelRequirement, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<HighLevelRequirement, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.HighLevelRequirements.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
