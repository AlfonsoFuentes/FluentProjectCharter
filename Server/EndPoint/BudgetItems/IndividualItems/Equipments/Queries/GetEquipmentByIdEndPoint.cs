using Server.EndPoint.Brands.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Equipments.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Nozzles.Queries;
using Shared.Enums.Materials;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Records;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Responses;
namespace Server.EndPoint.BudgetItems.IndividualItems.Equipments.Queries
{
    public static class GetEquipmentByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Equipments.EndPoint.GetById, async (GetEquipmentByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Equipment>, IIncludableQueryable<Equipment, object>> Includes = x => x
                    .Include(x => x.Nozzles)
                    .Include(x => x.EquipmentTemplate!).ThenInclude(x => x.BrandTemplate!)
                    .Include(x => x.EquipmentTemplate!).ThenInclude(x => x.NozzleTemplates);
                    Expression<Func<Equipment, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Equipments.Cache.GetById(request.Id);
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
        public static EquipmentResponse Map(this Equipment row)
        {
            EquipmentResponse result = new()
            {
                Id = row.Id,
                Name = row.Name,

                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
                BudgetUSD = row.BudgetUSD,
                TagLetter = row.TagLetter,
                TagNumber = row.TagNumber,
                BrandResponse = row.EquipmentTemplate == null || row.EquipmentTemplate!.BrandTemplate == null ? new() : row.EquipmentTemplate!.BrandTemplate!.Map(),
                Model = row.EquipmentTemplate == null ? string.Empty : row.EquipmentTemplate.Model,
                Reference = row.EquipmentTemplate == null ? string.Empty : row.EquipmentTemplate.Reference,
                InternalMaterial = row.EquipmentTemplate == null ? MaterialEnum.None : MaterialEnum.GetType(row.EquipmentTemplate.InternalMaterial),
                ExternalMaterial = row.EquipmentTemplate == null ? MaterialEnum.None : MaterialEnum.GetType(row.EquipmentTemplate.ExternalMaterial),
                Type = row.EquipmentTemplate == null ? string.Empty : row.EquipmentTemplate.Type,
                SubType = row.EquipmentTemplate == null ? string.Empty : row.EquipmentTemplate.SubType,
                ShowDetails = row.EquipmentTemplate == null ? false : true,
                Nozzles = row.Nozzles == null || row.Nozzles.Count == 0 ? new() : row.Nozzles.Select(x => x.Map()).ToList(),
                IsExisting = row.IsExisting,
                ProvisionalTag = row.ProvisionalTag,
                ShowProvisionalTag = !string.IsNullOrWhiteSpace(row.ProvisionalTag),
                ActualUSD = row.ActualUSD,
                CommitmentUSD = row.CommitmentUSD,
                PotentialUSD = row.PotentialUSD,
            };

            return result;
        }

    }
}
