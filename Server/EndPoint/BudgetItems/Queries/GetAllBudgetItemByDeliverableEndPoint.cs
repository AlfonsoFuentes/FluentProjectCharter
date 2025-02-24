using Shared.Models.BudgetItems.Records;
using Shared.Models.BudgetItems.Responses;
using Server.EndPoint.BudgetItems.IndividualItems.Valves.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Alterations.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Structurals.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Foundations.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Equipments.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Electricals.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Pipes.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Instruments.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.EHSs.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Paintings.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Taxs.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Testings.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.EngineeringDesigns.Queries;


namespace Server.EndPoint.Communications.Queries
{
    public static class GetAllBudgetItemByDeliverableEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.BudgetItems.EndPoint.GetAllByDeliverable, async (BudgetItemGetAllByDeliverable request, IQueryRepository repository) =>
                {
                    var rows = await GetBudgetItemAsync(request, repository);

                    if (rows == null)
                    {
                        return Result<BudgetItemResponseList>.Fail(
                            StaticClass.ResponseMessages.ReponseNotFound(StaticClass.BudgetItems.ClassLegend));
                    }

                    BudgetItemResponseList response = new()
                    {
                        Alterations = rows == null || rows.Count == 0 ? new() : rows.OfType<Alteration>().Select(x => x.Map()).ToList(),
                        Structurals = rows == null || rows.Count == 0 ? new() : rows.OfType<Structural>().Select(x => x.Map()).ToList(),
                        Foundations = rows == null || rows.Count == 0 ? new() : rows.OfType<Foundation>().Select(x => x.Map()).ToList(),
                        Equipments = rows == null || rows.Count == 0 ? new() : rows.OfType<Equipment>().Select(x => x.Map()).ToList(),

                        Valves = rows == null || rows.Count == 0 ? new() : rows.OfType<Valve>().Select(x => x.Map()).ToList(),
                        Electricals = rows == null || rows.Count == 0 ? new() : rows.OfType<Electrical>().Select(x => x.Map()).ToList(),
                        Pipings = rows == null || rows.Count == 0 ? new() : rows.OfType<Pipe>().Select(x => x.Map()).ToList(),
                        Instruments = rows == null || rows.Count == 0 ? new() : rows.OfType<Instrument>().Select(x => x.Map()).ToList(),

                        EHSs = rows == null || rows.Count == 0 ? new() : rows.OfType<EHS>().Select(x => x.Map()).ToList(),
                        Paintings = rows == null || rows.Count == 0 ? new() : rows.OfType<Painting>().Select(x => x.Map()).ToList(),
                        Taxes = rows == null || rows.Count == 0 ? new() : rows.OfType<Tax>().Select(x => x.Map()).ToList(),
                        Testings = rows == null || rows.Count == 0 ? new() : rows.OfType<Testing>().Select(x => x.Map()).ToList(),

                        EngineeringDesigns = rows == null || rows.Count == 0 ? new() : rows.OfType<EngineeringDesign>().Select(x => x.Map()).ToList(),
                       

                    };



                    return Result<BudgetItemResponseList>.Success(response);
                });
            }

            private static async Task<List<BudgetItem>> GetBudgetItemAsync(BudgetItemGetAllByDeliverable request, IQueryRepository repository)
            {
                //Func<IQueryable<BudgetItem>, IIncludableQueryable<BudgetItem, object>> includes = x => x;
                Expression<Func<BudgetItem, bool>> criteria = x => x.DeliverableId == request.DeliverableId;
                string cacheKey = StaticClass.BudgetItems.Cache.GetAllByDeliverable(request.DeliverableId);

                return await repository.GetAllAsync(Cache: cacheKey, Criteria: criteria);
            }


        }
    }
}