using Server.Database.Entities.BudgetItems.Commons;
using Server.EndPoint.Projects.Queries;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Records;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.EHSs.Queries
{
    public static class GetEHSByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EHSs.EndPoint.GetById, async (GetEHSByIdRequest request, IQueryRepository Repository) =>
                {
                    //Func<IQueryable<EHS>, IIncludableQueryable<EHS, object>> Includes = x => x.Include(x => x.Deliverable!);

                    ;

                    Expression<Func<EHS, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.EHSs.Cache.GetById(request.Id);
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
        public static EHSResponse Map(this EHS row)
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
