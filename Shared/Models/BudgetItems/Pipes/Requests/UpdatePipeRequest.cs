using Shared.Enums.CostCenter;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.OrganizationStrategies.Responses;
using Shared.Models.Templates.Pipes.Responses;

namespace Shared.Models.BudgetItems.Pipes.Requests
{
    public class UpdatePipeRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Pipes.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Pipes.ClassName;
        double BudgetCalculated => MaterialQuantity * EquivalentLenghPrice + LaborDayPrice * LaborQuantity;
        public double Budget { get; set; }
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
        public string Nomenclatore { get; set; } = string.Empty;
        public BrandResponse BrandResponse { get; set; } = new();
        public string Brand => BrandResponse == null ? string.Empty : BrandResponse.Name;
        public string TagNumber { get; set; } = string.Empty;
        public bool ShowDetails { get; set; } = false;
        public List<NozzleResponse> Nozzles { get; set; } = new();
        public string Tag => $"{Diameter.Name} - {FluidCodeCode} - {TagNumber} - {Material.Name} - {InsulationCode}";
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
                if (ShowDetails)
                    Budget = BudgetCalculated;
            }
        }
        public double LaborDayPrice
        {
            get { return laborDayPrice; }
            set
            {
                laborDayPrice = value;
                if (ShowDetails)
                    Budget = BudgetCalculated;
            }
        }
        public double EquivalentLenghPrice
        {
            get { return equivalentLenghPrice; }
            set
            {
                equivalentLenghPrice = value;
                if (ShowDetails)
                    Budget = BudgetCalculated;
            }
        }
        public double LaborQuantity
        {
            get { return laborQuantity; }
            set
            {
                laborQuantity = value;
                if (ShowDetails)
                    Budget = BudgetCalculated;
            }
        }
        public NominalDiameterEnum Diameter { get; set; } = NominalDiameterEnum.None;
        public EngineeringFluidCodeResponse? FluidCode { get; set; } = null!;
        public string FluidCodeName => FluidCode == null ? string.Empty : FluidCode.Name;
        public string FluidCodeCode => FluidCode == null ? string.Empty : FluidCode.Code;
        public MaterialEnum Material { get; set; } = MaterialEnum.None;
        public bool Insulation { get; set; }
        public string InsulationCode => Insulation ? "1" : "0";
        public PipeClassEnum PipeClass { get; set; } = PipeClassEnum.None;
        public bool IsExisting { get; set; }


    }
}
