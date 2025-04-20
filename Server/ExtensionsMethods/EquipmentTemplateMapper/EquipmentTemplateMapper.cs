using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.Templates.Equipments.Responses;
using Server.EndPoint.Brands.Queries;
using Server.EndPoint.PurchaseOrders.Queries;
namespace Server.ExtensionsMethods.EquipmentTemplateMapper
{
    public static class EquipmentTemplateMapper
    {
        public static EquipmentResponse Map(this Equipment row)
        {
           
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                GanttTaskId = row.GanttTaskId,
                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,

                TagLetter = row.TagLetter,
                TagNumber = row.TagNumber,
                Brand = row.EquipmentTemplate == null || row.EquipmentTemplate!.BrandTemplate == null ? new() : row.EquipmentTemplate!.BrandTemplate!.Map(),
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
                BudgetUSD = row.BudgetUSD,
                ActualUSD = row.ActualUSD,
                CommitmentUSD = row.CommitmentUSD,
                PotentialUSD = row.PotentialUSD,
                PurchaseOrders = row.PurchaseOrderItems == null ? new() : row.PurchaseOrderItems.Select(x => x.PurchaseOrder).Select(x => x.Map()).ToList(),
            };


        }
        public static Equipment Map(this EquipmentResponse request, Equipment row)
        {
            row.Name = request.Name;
            row.TagLetter = request.ShowDetails ? request.TagLetter : string.Empty;
            row.TagNumber = request.TagNumber;
            row.IsExisting = request.IsExisting;
            row.BudgetUSD = request.BudgetUSD;
            row.ProvisionalTag = request.ProvisionalTag;
            return row;
        }
        public static EquipmentTemplate Map(this EquipmentTemplateResponse request, EquipmentTemplate row)
        {

            row.Value = request.Value;
            row.TagLetter = request.TagLetter;
            row.Reference = request.Reference;
            row.InternalMaterial = request.InternalMaterial.Id;
            row.ExternalMaterial = request.ExternalMaterial.Id;
            row.Model = request.Model;
            row.BrandTemplateId = request.Brand!.Id;
            row.SubType = request.SubType;
            row.Type = request.Type;

            return row;
        }
        public static EquipmentTemplateResponse Map(this EquipmentTemplate row)
        {
            return new()
            {
                Value = row.Value,
                Id = row.Id,
                TagLetter = row.TagLetter,
                Reference = row.Reference,
                Brand = row.BrandTemplate == null ? null : new BrandResponse()
                {
                    Id = row.BrandTemplate.Id,
                    Name = row.BrandTemplate.Name
                },
                ExternalMaterial = MaterialEnum.GetType(row.ExternalMaterial),
                InternalMaterial = MaterialEnum.GetType(row.InternalMaterial),
                Model = row.Model,
                Name = $"{row.Type} {row.Model}",
                Type = row.Type,
                SubType = row.SubType,
                Nozzles = row.NozzleTemplates.Select(x => x.Map()).ToList(),
            };


        }

        public static EquipmentTemplate Map(this EquipmentResponse request, EquipmentTemplate row, double Value)
        {

            row.Value = Value;
            row.TagLetter = request.TagLetter;
            row.Reference = request.Reference;
            row.InternalMaterial = request.InternalMaterial.Id;
            row.ExternalMaterial = request.ExternalMaterial.Id;
            row.Model = request.Model;
            row.BrandTemplateId = request.Brand!.Id;
            row.SubType = request.SubType;
            row.Type = request.Type;

            return row;
        }

        public static async Task<EquipmentTemplate> GetEquipmentTemplate(IRepository Repository, EquipmentResponse Data)
        {
            Func<IQueryable<EquipmentTemplate>, IIncludableQueryable<EquipmentTemplate, object>> Includes = x => x

                 .Include(x => x.BrandTemplate!)
                 .Include(x => x.NozzleTemplates)
                  ;

            Expression<Func<EquipmentTemplate, bool>> CriteriaExist = x =>
            x.InternalMaterial == Data.InternalMaterial.Id &&
            x.ExternalMaterial == Data.ExternalMaterial.Id &&
            x.BrandTemplateId == Data.BrandId &&
            x.Model.Equals(Data.Model) &&
            x.Type.Equals(Data.Type) &&
            x.SubType.Equals(Data.SubType) &&
            x.TagLetter.Equals(Data.TagLetter) &&
            x.Reference.Equals(Data.Reference);
            var equipmentTemplates = await Repository.GetAllAsync(Includes: Includes, Criteria: CriteriaExist);
            if (equipmentTemplates != null && equipmentTemplates.Any())
            {
                foreach (var item in equipmentTemplates)
                {

                    if (item.NozzleTemplates.ValidateNozzles(Data.Nozzles))
                    {
                        return item; // Si todas las boquillas coinciden, retornar true
                    }
                }

            }

            var equipmentTemplate = Template.AddEquipmentTemplate();
            Data.Map(equipmentTemplate, Data.BudgetUSD);
            foreach (var nozzle in Data.Nozzles)
            {
                var nozzleTemplate = NozzleTemplate.Create(equipmentTemplate.Id);
                nozzle.Map(nozzleTemplate);
                await Repository.AddAsync(nozzleTemplate);
            }
            await Repository.AddAsync(equipmentTemplate);
            return equipmentTemplate;

        }

    }
}
