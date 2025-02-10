using Shared.Models.Milestones.Validators;
using Shared.Models.Backgrounds.Validators;

namespace Server.EndPoint.Milestones.Validations
{
    public static class ValidateMilestonesNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Milestones.EndPoint.Validate, async (ValidateMilestoneRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Milestone, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;

                    Func<Milestone, bool> CriteriaExist = x => Data.Id == null ?

                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Milestones.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
