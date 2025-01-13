using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Valves
{
    public class Valve : EngineeringItem
    {
        public override string Letter { get; set; } = "D";
        public string SerialNumber { get; set; } = string.Empty;

        public ValveTemplate? ValveTemplate { get; set; } = null!;
        public Guid? ValveTemplateId { get; set; }
        public static Valve Create(Guid ProjectId, Guid DeliverableId)
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
