using Server.Database.Entities.BudgetItems.Commons;
using Server.EndPoint.Projects.Queries;
using Shared.Models.BudgetItems.Paintings.Records;
using Shared.Models.BudgetItems.Paintings.Responses;

namespace Server.EndPoint.Paintings.Queries
{
    public static class GetPaintingByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Paintings.EndPoint.GetById, async (GetPaintingByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Painting>, IIncludableQueryable<Painting, object>> Includes = x => x.Include(x => x.Deliverable!);

                    ;

                    Expression<Func<Painting, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Paintings.Cache.GetById(request.Id);
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
        public static PaintingResponse Map(this Painting row)
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
