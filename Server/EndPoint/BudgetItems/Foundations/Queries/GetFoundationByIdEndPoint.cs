using Server.Database.Entities.BudgetItems.Commons;
using Server.EndPoint.Projects.Queries;
using Shared.Models.BudgetItems.Foundations.Records;
using Shared.Models.BudgetItems.Foundations.Responses;

namespace Server.EndPoint.Foundations.Queries
{
    public static class GetFoundationByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Foundations.EndPoint.GetById, async (GetFoundationByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Foundation>, IIncludableQueryable<Foundation, object>> Includes = x => x.Include(x => x.Deliverable!);

                    ;

                    Expression<Func<Foundation, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Foundations.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map();
                    return Result.Success(response);

                });
            }
        }
        public static FoundationResponse Map(this Foundation row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId == null ? Guid.Empty : row.DeliverableId.Value,
                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
                UnitaryCost = row.UnitaryCost,
                Quantity = row.Quantity,
            };
        }


    }
}
