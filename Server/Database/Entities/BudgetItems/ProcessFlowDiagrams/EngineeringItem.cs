using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Enums;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Nozzles;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams
{
    public abstract class EngineeringItem : BudgetItem
    {

        public string TagNumber { get; set; } = string.Empty;
        public string TagLetter { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;

        public EngineeringItemType Type { get; set; } = EngineeringItemType.None;
        public ProcessFlowDiagram? ProcessFlowDiagram { get; set; } = null!;
        public Guid? ProcessFlowDiagramId {  get; set; } 
        public ICollection<Nozzle> Nozzles { get; set; } = new List<Nozzle>();

        [ForeignKey("ItemConnectedId")]
        public ICollection<Nozzle> ItemConnecteds { get; set; } = new List<Nozzle>();


    }

}
