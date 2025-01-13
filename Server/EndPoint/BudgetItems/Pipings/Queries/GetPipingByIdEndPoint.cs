using Server.Database.Entities.BudgetItems.Commons;
using Server.EndPoint.Projects.Queries;
using Shared.Models.BudgetItems.Pipings.Records;
using Shared.Models.BudgetItems.Pipings.Responses;

namespace Server.EndPoint.Pipings.Queries
{
    public static class GetPipingByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipings.EndPoint.GetById, async (GetPipingByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Isometric>, IIncludableQueryable<Isometric, object>> Includes = x => x.Include(x => x.Deliverable!);

                    ;

                    Expression<Func<Isometric, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Pipings.Cache.GetById(request.Id);
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
        public static PipingResponse Map(this Isometric row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId == null ? Guid.Empty : row.DeliverableId.Value,
                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
                MaterialUnitaryCost = row.MaterialUnitaryCost,
                MaterialQuantity = row.MaterialQuantity,
                LaborUnitaryCost = row.LaborUnitaryCost,
                LaborQuantity = row.LaborQuantity,
                Diameter = row.Diameter,
                Material = row.Material,
                Insulation = row.Insulation,
                FluidCodeName = row.FluidCodeName,
                TagNumber = row.TagNumber,


            };
        }


    }
}
