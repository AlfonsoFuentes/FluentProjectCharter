using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Valves
{
    public class Valve : EngineeringItem
    {
        public override string Letter { get; set; } = "V";
        public string SerialNumber { get; set; } = string.Empty;

        public ValveTemplate? ValveTemplate { get; set; } = null!;
        public Guid? ValveTemplateId { get; set; }
        [NotMapped]
        public override int OrderList => 5;
        public static Valve Create(Guid ProjectId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ProjectId = ProjectId,
                

            };
        }
    }

}
