using Server.Database.Entities.BudgetItems.Commons;
using Server.EndPoint.Projects.Queries;
using Shared.Models.BudgetItems.Taxs.Records;
using Shared.Models.BudgetItems.Taxs.Responses;

namespace Server.EndPoint.Taxs.Queries
{
    public static class GetTaxByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Taxs.EndPoint.GetById, async (GetTaxByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Tax>, IIncludableQueryable<Tax, object>> Includes = x => x.Include(x => x.Deliverable!);

                    ;

                    Expression<Func<Tax, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Taxs.Cache.GetById(request.Id);
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
        public static TaxResponse Map(this Tax row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId == null ? Guid.Empty : row.DeliverableId.Value,
                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
             
            };
        }


    }
}
