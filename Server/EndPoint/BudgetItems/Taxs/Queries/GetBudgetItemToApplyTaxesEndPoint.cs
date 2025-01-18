using Shared.Models.BudgetItems.Taxs.Records;
using Shared.Models.BudgetItems.Taxs.Responses;

using Server.EndPoint.Alterations.Queries;
using Server.EndPoint.EHSs.Queries;
using Server.EndPoint.Electricals.Queries;
using Server.EndPoint.EngineeringDesigns.Queries;
using Server.EndPoint.Equipments.Queries;
using Server.EndPoint.Foundations.Queries;
using Server.EndPoint.Instruments.Queries;
using Server.EndPoint.Paintings.Queries;
using Server.EndPoint.StakeHolders.Queries;
using Server.EndPoint.Structurals.Queries;
using Server.EndPoint.Taxs.Queries;
using Server.EndPoint.Testings.Queries;
using Server.EndPoint.Valves.Queries;
using Server.EndPoint.Pipes.Queries;
using Shared.Models.BudgetItems.Taxs.Requests;

namespace Server.EndPoint.Taxs.Queries
{
    public static class GetBudgetItemToApplyTaxesEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Taxs.EndPoint.GetBudgetItemsToApplyTaxById, async (GetBudgetItemsToApplyTaxRequest request, IQueryRepository Repository) =>
                {

                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x.Include(x => x.BudgetItems);

                    Expression<Func<Project, bool>> Criteria = x => x.Id == request.ProjectId;

                    string CacheKey = StaticClass.Projects.Cache.GetById(request.ProjectId);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria, Includes: Includes);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }


                    var items = row.BudgetItems.Where(x =>
                        x.GetType() == typeof(Structural) ||
                        x.GetType() == typeof(Foundation) ||
                        x.GetType() == typeof(Equipment) ||
                        x.GetType() == typeof(Electrical) ||
                        x.GetType() == typeof(Pipe) ||
                        x.GetType() == typeof(Valve) ||
                        x.GetType() == typeof(Instrument) ||
                        x.GetType() == typeof(EHS) ||
                        x.GetType() == typeof(Painting) ||
                        x.GetType() == typeof(Testing) ||
                        x.GetType() == typeof(EngineeringDesign)
                        ).Select(x => x.Map()).ToList();
            


                    TaxItemResponseList response = new TaxItemResponseList()
                    {
                        Items= items,


                    };
                    return Result<TaxItemResponseList>.Success(response);


                });
            }


        }
        static TaxItemResponse Map(this BudgetItem row)
        {
            return new()
            {
                Budget = row.Budget,
                BudgetItemId = row.Id,
               
                Name = row.Name,
                Nomenclatore = row.Nomenclatore,
            };

        }


    }
}
