using Server.Database.Contracts;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Pipings
{
    public class Isometric : EngineeringItem
    {
        [NotMapped]
        public override string Tag => $"{Diameter}-{FluidCodeName}-{TagNumber}-{Material}-{InsulationCode}";
        public override string Letter { get; set; } = "F";
        public double MaterialUnitaryCost { get; set; }
        public double MaterialQuantity { get; set; }
        public double LaborUnitaryCost { get; set; }
        public double LaborQuantity { get; set; }
        public string Diameter { get; set; } = string.Empty;
        public EngineeringFluidCode? FluidCode { get; set; } = null!;
        public Guid? FluidCodeId { get; set; } 
        public string FluidCodeName { get; set; } = string.Empty;
      
        public string Material { get; set; } = string.Empty;
        public bool Insulation { get; set; }
        [NotMapped]
        public string InsulationCode => Insulation ? "1" : "0";
        public List<IsometricItem> IsometricItems { get; set; } = new List<IsometricItem>();
        public static Isometric Create(Guid ProjectId, Guid DeliverableId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ProjectId = ProjectId,
                DeliverableId = DeliverableId,
            };
        }
    }

}
