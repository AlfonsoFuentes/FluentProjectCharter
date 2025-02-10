using Server.Database.Entities.BudgetItems.Commons;
using Server.EndPoint.Projects.Queries;
using Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Records;
using Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.EngineeringDesigns.Queries
{
    public static class GetEngineeringDesignByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EngineeringDesigns.EndPoint.GetById, async (GetEngineeringDesignByIdRequest request, IQueryRepository Repository) =>
                {
                    //Func<IQueryable<EngineeringDesign>, IIncludableQueryable<EngineeringDesign, object>> Includes = x => x.Include(x => x.Deliverable!);

                    //;

                    Expression<Func<EngineeringDesign, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.EngineeringDesigns.Cache.GetById(request.Id);
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
        public static EngineeringDesignResponse Map(this EngineeringDesign row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,

                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
                Budget = row.Budget
            };
        }


    }
}
