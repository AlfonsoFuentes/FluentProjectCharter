using Server.Database.Entities.BudgetItems.Commons;
using Server.EndPoint.Projects.Queries;
using Shared.Models.BudgetItems.Testings.Records;
using Shared.Models.BudgetItems.Testings.Responses;

namespace Server.EndPoint.Testings.Queries
{
    public static class GetTestingByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Testings.EndPoint.GetById, async (GetTestingByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Testing>, IIncludableQueryable<Testing, object>> Includes = x => x.Include(x => x.Deliverable!);

                    ;

                    Expression<Func<Testing, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Testings.Cache.GetById(request.Id);
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
        public static TestingResponse Map(this Testing row)
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
