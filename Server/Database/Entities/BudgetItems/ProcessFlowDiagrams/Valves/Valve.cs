using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Valves
{
    public class Valve : EngineeringItem
    {

        public string SerialNumber { get; set; } = string.Empty;

        public ValveTemplate? ValveTemplate { get; set; } = null!;
        public Guid? ValveTemplateId { get; set; }

    }

}
