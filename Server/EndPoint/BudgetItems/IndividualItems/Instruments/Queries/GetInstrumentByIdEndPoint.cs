using Server.EndPoint.Brands.Queries;
using Server.EndPoint.PurchaseOrders.Queries;
using Server.ExtensionsMethods.InstrumentTemplateMapper;
using Shared.Enums.ConnectionTypes;
using Shared.Enums.Instruments;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.BudgetItems.IndividualItems.Instruments.Records;
using Shared.Models.BudgetItems.IndividualItems.Instruments.Responses;
namespace Server.EndPoint.BudgetItems.IndividualItems.Instruments.Queries
{
    public static class GetInstrumentByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Instruments.EndPoint.GetById, async (GetInstrumentByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Instrument>, IIncludableQueryable<Instrument, object>> Includes = x => x
                    .Include(x => x.Nozzles)
                    .Include(x => x.InstrumentTemplate!).ThenInclude(x => x.BrandTemplate!)
               ;
                    Expression<Func<Instrument, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Instruments.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria, Includes: Includes);



                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }



                    var response = row.Map();

                    return Result.Success(response);

                });
            }
        }
        public static InstrumentResponse Map(this Instrument row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                //GanttTaskId = row.GanttTaskId,
                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
         

                TagNumber = row.TagNumber,
                Template = row.InstrumentTemplate == null ? new() : row.InstrumentTemplate.Map(),
               
                ShowDetails = row.InstrumentTemplate == null ? false : true,
                Nozzles = row.Nozzles == null || row.Nozzles.Count == 0 ? new() : row.Nozzles.Select(x => x.Map()).ToList(),
                         
                IsExisting = row.IsExisting,
                ProvisionalTag = row.ProvisionalTag,
                ShowProvisionalTag = !string.IsNullOrWhiteSpace(row.ProvisionalTag),
                BudgetUSD = row.BudgetUSD,
                ActualUSD = row.ActualUSD,
                CommitmentUSD = row.CommitmentUSD,
                PotentialUSD = row.PotentialUSD,
                PurchaseOrders = row.PurchaseOrderItems == null ? new() : row.PurchaseOrderItems.Select(x => x.PurchaseOrder).Select(x => x.Map()).ToList(),
            };


        }

    }
}
