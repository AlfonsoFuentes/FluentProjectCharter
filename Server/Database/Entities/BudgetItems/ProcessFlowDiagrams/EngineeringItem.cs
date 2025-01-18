using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Enums;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Nozzles;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams
{
    public abstract class EngineeringItem : BudgetItem
    {

        public string TagNumber { get; set; } = string.Empty;
        public string TagLetter { get; set; } = string.Empty;
        [NotMapped]
        public virtual string Tag => $"{TagLetter}-{TagNumber}";
        public ICollection<Nozzle> Nozzles { get; set; } = new List<Nozzle>();

        [ForeignKey("ItemConnectedId")]
        public ICollection<Nozzle> ItemConnecteds { get; set; } = new List<Nozzle>();


    }

}
