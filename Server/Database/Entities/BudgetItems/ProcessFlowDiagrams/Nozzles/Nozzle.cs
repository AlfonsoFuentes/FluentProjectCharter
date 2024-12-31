using Server.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Nozzles
{
    public class Nozzle : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public EngineeringItem EngineeringItem { get; set; } = null!;
        public Guid EngineeringItemId { get; set; }
        public EngineeringItem? ItemConnected { get; set; } = null!;
        public Guid? ItemConnectedId { get; set; }
        [NotMapped]
        public string Name => $"N{Order}";
        public string EndType { get; set; } = string.Empty;
        public string NominalDiameter { get; set; } = string.Empty;
        public string WeldType { get; set; } = string.Empty;
        public double OuterDiameter { get; set; }
        public double Thickness { get; set; }
        public string OuterDiameterUnit { get; set; } = string.Empty;
        public string ThicknessUnit { get; set; } = string.Empty;
        public string HeightDiameterUnit { get; set; } = string.Empty;
        public string HeightUnit { get; set; } = string.Empty;
        public int Order { get; set; }


    }

}
