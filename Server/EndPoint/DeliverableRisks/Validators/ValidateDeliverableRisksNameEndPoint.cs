using Shared.Models.DeliverableRisks.Validators;
using Shared.Models.Backgrounds.Validators;

namespace Server.EndPoint.DeliverableRisks.Vallidators
{
    public static class ValidateDeliverableRisksNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.DeliverableRisks.EndPoint.Validate, async (ValidateDeliverableRiskRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<DeliverableRisk, bool>> CriteriaId = x => x.DeliverableId == Data.DeliverableId;
                    Func<DeliverableRisk, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.DeliverableRisks.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
