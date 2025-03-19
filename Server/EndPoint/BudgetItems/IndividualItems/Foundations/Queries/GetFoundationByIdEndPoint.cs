using Server.Database.Entities.BudgetItems.Commons;
using Server.EndPoint.Projects.Queries;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Records;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Foundations.Queries
{
    public static class GetFoundationByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Foundations.EndPoint.GetById, async (GetFoundationByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Foundation>, IIncludableQueryable<Foundation, object>> Includes = x => null!;

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

                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
                UnitaryCost = row.UnitaryCost,
                Quantity = row.Quantity,
                ActualUSD = row.ActualUSD,
                CommitmentUSD = row.CommitmentUSD,
                PotentialUSD = row.PotentialUSD,
            };
        }


    }
}
