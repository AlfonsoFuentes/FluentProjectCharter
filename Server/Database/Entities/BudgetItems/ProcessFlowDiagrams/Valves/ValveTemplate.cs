using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Valves
{
    public class ValveTemplate : Template
    {
        public string Model { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string ActuatorType { get; set; } = string.Empty;
        public string PositionerType { get; set; } = string.Empty;
        public bool HasFeedBack { get; set; }
        public string Diameter { get; set; } = string.Empty;
        public string FailType { get; set; } = string.Empty;
        public string SignalType { get; set; } = string.Empty;
        public double Value { get; set; }

        [ForeignKey("ValveTemplateId")]
        public List<Valve> Valves { get; set; } = new();
    }

}
