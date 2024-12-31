using Server.Database.Contracts;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Pipings
{
    public class Isometric : EngineeringItem
    {

        public override string Letter { get; set; } = "F";
        public double MaterialUnitaryCost { get; set; }
        public double MaterialQuantity { get; set; }
        public double LaborUnitaryCost { get; set; }
        public double LaborQuantity { get; set; }
        public string Diameter { get; set; } = string.Empty;
        public EngineeringFluidCode? FluidCode { get; set; } = null!;
        public Guid? FluidCodeId { get; set; } = Guid.Empty;
        public string Material { get; set; } = string.Empty;
        public bool Insulation { get; set; }
        public List<IsometricItem> IsometricItems { get; set; } = new List<IsometricItem>();
    }
    public class EngineeringFluidCode : AuditableEntity<Guid>, ITenantCommon
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        [ForeignKey("FluidCodeId")]

        public ICollection<Isometric> Isometrics { get; set; } = new List<Isometric>();

        public static EngineeringFluidCode Create()
        {
            return new EngineeringFluidCode()
            {
                Id = Guid.NewGuid(),
            };
        }
    }

}
