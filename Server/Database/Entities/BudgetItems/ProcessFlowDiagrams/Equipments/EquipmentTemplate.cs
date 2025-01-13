using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Equipments
{
    public class EquipmentTemplate : Template
    {
        public string Reference { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string InternalMaterial { get; set; } = string.Empty;
        public string ExternalMaterial { get; set; } = string.Empty;
        public double Value { get; set; }

        [ForeignKey("EquipmentTypeId")]
        public List<Equipment> Equipments { get; set; } = new();
       
    }

}
