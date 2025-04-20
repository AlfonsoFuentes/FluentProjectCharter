using Server.EndPoint.Brands.Queries;
using Server.EndPoint.PurchaseOrders.Queries;
using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.BudgetItems.IndividualItems.Valves.Responses;
using Shared.Models.Templates.Valves.Responses;

namespace Server.ExtensionsMethods.ValveTemplateMappers
{
    public static class ValveTemplateMapper
    {
        public static ValveTemplate Map(this ValveTemplateResponse request, ValveTemplate row)
        {
            row.Value = request.Value;
            row.TagLetter = request.TagLetter;

            row.Model = request.Model;
            row.BrandTemplateId = request.Brand!.Id;

            row.Type = request.Type.Id;
            row.HasFeedBack = request.HasFeedBack;
            row.Diameter = request.Diameter.Id;
            row.ActuatorType = request.ActuatorType.Id;
            row.FailType = request.FailType.Id;
            row.Material = request.Material.Id;
            row.PositionerType = request.PositionerType.Id;
            row.SignalType = request.SignalType.Id;
  

            return row;
        }
        public static ValveTemplateResponse Map(this ValveTemplate row)
        {
            return new()
            {
                Id = row.Id,
                Brand = row.BrandTemplate == null ? new() : row.BrandTemplate.Map(),
                Material = MaterialEnum.GetType(row.Material),
                Model = row.Model,
                ActuatorType = ActuatorTypeEnum.GetType(row.ActuatorType),
                Type = ValveTypesEnum.GetType(row.Type),
                Diameter = NominalDiameterEnum.GetType(row.Diameter),
                FailType = FailTypeEnum.GetType(row.FailType),
                HasFeedBack = row.HasFeedBack,
                SignalType = SignalTypeEnum.GetType(row.SignalType),
                TagLetter = row.TagLetter,
                PositionerType = PositionerTypeEnum.GetType(row.PositionerType),
              

                Value = row.Value,
                Nozzles = row.NozzleTemplates.Count == 0 ? new() : row.NozzleTemplates.Select(x => x.Map()).ToList(),

            };
        }

        public static ValveTemplate Map(this ValveResponse request, ValveTemplate row, double Value)
        {

            row.Value = Value;
            row.TagLetter = request.TagLetter;

            row.Model = request.Model;
            row.BrandTemplateId = request.Brand!.Id;

            row.Type = request.Type.Id;
            row.HasFeedBack = request.HasFeedBack;
            row.Diameter = request.Diameter.Id;
            row.ActuatorType = request.ActuatorType.Id;
            row.FailType = request.FailType.Id;
            row.Material = request.Material.Id;
            row.PositionerType = request.PositionerType.Id;
            row.SignalType = request.SignalType.Id;
   

            return row;
        }
        public static Valve Map(this ValveResponse request, Valve row)
        {
            row.Name = request.Name;
            row.TagLetter = request.TagLetter;
            row.TagNumber = request.TagNumber;
            row.IsExisting = request.IsExisting;
            row.BudgetUSD = request.BudgetUSD;
            row.ProvisionalTag = request.ProvisionalTag;
            return row;
        }
       public static async Task<ValveTemplate> GetValveTemplate(IRepository Repository, ValveResponse Data)
        {
            Func<IQueryable<ValveTemplate>, IIncludableQueryable<ValveTemplate, object>> Includes = x => x
           .Include(x => x.BrandTemplate!)
           .Include(x => x.NozzleTemplates)
           ;
            Expression<Func<ValveTemplate, bool>> Criteria = x =>
            x.BrandTemplateId == Data.BrandId &&
            x.Model.Equals(Data.Model) &&
            
            x.Type == Data.Type.Id &&
            x.Material == Data.Material.Id &&
            x.Diameter == Data.Diameter.Id &&
            x.ActuatorType == Data.ActuatorType.Id &&
            x.PositionerType == Data.PositionerType.Id &&
            x.HasFeedBack == Data.HasFeedBack &&
            x.FailType == Data.FailType.Id &&
       
            x.SignalType == Data.SignalType.Id;
            var valveTemplates = await Repository.GetAllAsync(Includes: Includes, Criteria: Criteria);
            if (valveTemplates != null && valveTemplates.Any())
            {
                foreach (var item in valveTemplates)
                {

                    if (item.NozzleTemplates.ValidateNozzles(Data.Nozzles))
                    {
                        return item; // Si todas las boquillas coinciden, retornar true
                    }
                }

            }

            var valveTemplate = Template.AddValveTemplate();
            Data.Map(valveTemplate, Data.BudgetUSD);
            foreach (var nozzle in Data.Nozzles)
            {
                var nozzleTemplate = NozzleTemplate.Create(valveTemplate.Id);
                nozzle.Map(nozzleTemplate);
                await Repository.AddAsync(nozzleTemplate);
            }
            await Repository.AddAsync(valveTemplate);   
            return valveTemplate;

        }
        public static ValveResponse Map(this Valve row)
        {
            ValveResponse result = new()
            {
                Id = row.Id,
                Name = row.Name,
                GanttTaskId = row.GanttTaskId,
                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
               

                TagNumber = row.TagNumber,
                Brand = row.ValveTemplate == null || row.ValveTemplate.BrandTemplate == null ? new() : row.ValveTemplate.BrandTemplate!.Map(),
                Model = row.ValveTemplate == null ? string.Empty : row.ValveTemplate.Model,
                Type = row.ValveTemplate == null ? ValveTypesEnum.None : ValveTypesEnum.GetType(row.ValveTemplate.Type),
                Material = row.ValveTemplate == null ? MaterialEnum.None : MaterialEnum.GetType(row.ValveTemplate.Material),
                ActuatorType = row.ValveTemplate == null ? ActuatorTypeEnum.None : ActuatorTypeEnum.GetType(row.ValveTemplate.ActuatorType),
                PositionerType = row.ValveTemplate == null ? PositionerTypeEnum.None : PositionerTypeEnum.GetType(row.ValveTemplate.PositionerType),
                HasFeedBack = row.ValveTemplate == null ? false : row.ValveTemplate.HasFeedBack,
                Diameter = row.ValveTemplate == null ? NominalDiameterEnum.None : NominalDiameterEnum.GetType(row.ValveTemplate.Diameter),
                FailType = row.ValveTemplate == null ? FailTypeEnum.None : FailTypeEnum.GetType(row.ValveTemplate.FailType),
                SignalType = row.ValveTemplate == null ? SignalTypeEnum.None : SignalTypeEnum.GetType(row.ValveTemplate.SignalType),
                TagLetter = row.TagLetter,
                ShowDetails = row.ValveTemplate != null,
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

            return result;
        }
    }
}
