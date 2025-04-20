using Server.EndPoint.Brands.Queries;
using Server.EndPoint.EngineeringFluidCodes.Queries;
using Server.EndPoint.PurchaseOrders.Queries;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Models.BudgetItems.IndividualItems.Pipes.Responses;
using Shared.Models.Templates.Pipings.Responses;

namespace Server.ExtensionsMethods.Pipings
{
    public static class PipingMapper
    {
        public static PipeTemplate Map(this PipeTemplateResponse request, PipeTemplate row)
        {

            row.Diameter = request.Diameter.Id;
            row.Class = request.Class.Id;
            row.EquivalentLenghPrice = request.EquivalentLenghPrice;
            row.LaborDayPrice = request.LaborDayPrice;
            row.Insulation = request.Insulation;
            row.Material = request.Material.Id;


            return row;
        }
        public static PipeTemplate Map(this PipeResponse request, PipeTemplate row)
        {
     
            row.Diameter = request.Diameter.Id;
            row.Class = request.PipeClass.Id;
            row.EquivalentLenghPrice = request.EquivalentLenghPrice;
            row.LaborDayPrice = request.LaborDayPrice;
            row.Insulation = request.Insulation;
            row.Material = request.Material.Id;


            return row;
        }
        public static PipeTemplateResponse Map(this PipeTemplate row)
        {
            return new()
            {
                Id = row.Id,
        
                Material = MaterialEnum.GetType(row.Material),
                Class = PipeClassEnum.GetType(row.Class),
                Diameter = NominalDiameterEnum.GetType(row.Diameter),
                EquivalentLenghPrice = row.EquivalentLenghPrice,
                Insulation = row.Insulation,
                LaborDayPrice = row.LaborDayPrice,




            };
        }

        public static Pipe Map(this PipeResponse request, Pipe row)
        {
            row.Name = request.Name;
            row.BudgetUSD = request.BudgetUSD;
            row.FluidCodeId = request.FluidCode == null ? null : request.FluidCode.Id;
            row.LaborQuantity = request.LaborQuantity;
            row.MaterialQuantity = request.MaterialQuantity;
            row.TagNumber = request.TagNumber;
            row.Material = request.Material.Id;
            row.Diameter = request.Diameter.Id;
            row.FluidCodeCode = request.FluidCode!.Code;
            row.Insulation = request.Insulation;
            row.LaborDayPrice = request.LaborDayPrice;
            row.EquivalentLenghPrice = request.EquivalentLenghPrice;
            row.IsExisting = request.IsExisting;

            return row;
        }
        public static PipeResponse Map(this Pipe row)
        {
            PipeResponse result = new()
            {
                Id = row.Id,
                Name = row.Name,
                GanttTaskId = row.GanttTaskId,
                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,

                TagNumber = row.TagNumber,
               
                ShowDetails = row.PipeTemplate != null,

                Diameter = NominalDiameterEnum.GetType(row.Diameter),
                EquivalentLenghPrice = row.PipeTemplate == null ? row.EquivalentLenghPrice : row.PipeTemplate.EquivalentLenghPrice,
                Insulation = row.Insulation,
                LaborDayPrice = row.PipeTemplate == null ? row.LaborDayPrice : row.PipeTemplate.LaborDayPrice,
                Material = MaterialEnum.GetType(row.Material),
                LaborQuantity = row.LaborQuantity,
                MaterialQuantity = row.MaterialQuantity,
                FluidCode = row.FluidCode == null ? null : row.FluidCode.Map(),
                PipeClass = row.PipeTemplate == null ? PipeClassEnum.None : PipeClassEnum.GetType(row.PipeTemplate.Class),
         
                BudgetUSD = row.BudgetUSD,
                IsExisting = row.IsExisting,
                ActualUSD = row.ActualUSD,
                CommitmentUSD = row.CommitmentUSD,
                PotentialUSD = row.PotentialUSD,
                PurchaseOrders = row.PurchaseOrderItems == null ? new() : row.PurchaseOrderItems.Select(x => x.PurchaseOrder).Select(x => x.Map()).ToList(),

            };
            return result;

        }
        public static async Task<PipeTemplate> GetPipeTemplate(IRepository Repository, PipeResponse Data)
        {
           
            Expression<Func<PipeTemplate, bool>> Criteria = x =>
           
           x.Insulation == Data.Insulation &&
           x.Material==Data.Material.Id&&
           x.Diameter==Data.Diameter.Id &&
           x.Class == Data.PipeClass.Id;
            var pipeTemplate = await Repository.GetAsync(Criteria: Criteria);
            if (pipeTemplate == null)
            {
                pipeTemplate = Template.AddPipeTemplate();
                Data.Map(pipeTemplate);
                await Repository.AddAsync(pipeTemplate);    

            }


            return pipeTemplate;

        }
    }
}
