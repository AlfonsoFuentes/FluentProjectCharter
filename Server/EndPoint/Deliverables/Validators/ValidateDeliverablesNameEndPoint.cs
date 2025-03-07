using Shared.Models.Deliverables.Validators;
using Shared.Models.Backgrounds.Validators;
using Server.Database.Entities.ProjectManagements;

namespace Server.EndPoint.Deliverables.Validators
{
    public static class ValidateDeliverablesNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.Validate, async (ValidateDeliverableRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Deliverable, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Deliverable, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Deliverables.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
