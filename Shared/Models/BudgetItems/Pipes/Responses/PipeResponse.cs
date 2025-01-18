using Shared.Enums.BudgetItemTypes;
using Shared.Enums.CostCenter;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Shared.Models.OrganizationStrategies.Responses;
using Shared.Models.Templates.Pipes.Responses;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using static Shared.StaticClasses.StaticClass;

namespace Shared.Models.BudgetItems.Pipes.Responses
{
    public class PipeResponse : BaseResponse, IBudgetItemResponse
    {
        public string UpadtePageName { get; set; } = StaticClass.Pipes.PageName.Update;
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
        public double Budget { get; set; }
        public double BudgetCalculated => MaterialQuantity * EquivalentLenghPrice + LaborDayPrice * LaborQuantity;
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
        public string Nomenclatore { get; set; } = string.Empty;
        public BrandResponse BrandResponse { get; set; } = new();
        public string Brand => BrandResponse == null ? string.Empty : BrandResponse.Name;
        public string TagNumber { get; set; } = string.Empty;
        public bool ShowDetails { get; set; } = false;
        public List<NozzleResponse> Nozzles { get; set; } = new();
        public string Tag => $"{Diameter.Name}-{FluidCodeNameCode}-{TagNumber}-{Material.Name}-{InsulationCode}";
        double materialQuantity;
        double laborDayPrice;
        double equivalentLenghPrice;
        double laborQuantity;


        public double MaterialQuantity
        {
            get
            {
                return materialQuantity;
            }
            set
            {
                materialQuantity = value;
                Budget = BudgetCalculated;
            }
        }
        public double LaborDayPrice
        {
            get { return laborDayPrice; }
            set
            {
                laborDayPrice = value;
                Budget = BudgetCalculated;
            }
        }
        public double EquivalentLenghPrice
        {
            get { return equivalentLenghPrice; }
            set
            {
                equivalentLenghPrice = value;
                Budget = BudgetCalculated;
            }
        }
        public double LaborQuantity
        {
            get { return laborQuantity; }
            set
            {
                laborQuantity = value;
                Budget = BudgetCalculated;
            }
        }
        public NominalDiameterEnum Diameter { get; set; } = NominalDiameterEnum.None;
        public EngineeringFluidCodeResponse? FluidCode { get; set; } = null!;
        public string FluidCodefromDB { get; set; } = string.Empty;
        public string FluidCodeName => FluidCode == null ? string.Empty : FluidCode.Name;
        public string FluidCodeNameCode => FluidCode == null ? FluidCodefromDB : FluidCode.Code;
        public MaterialEnum Material { get; set; } = MaterialEnum.None;
        public bool Insulation { get; set; }
        public string InsulationCode => Insulation ? "1" : "0";
        public PipeClassEnum PipeClass { get; set; } = PipeClassEnum.None;


    }
}
