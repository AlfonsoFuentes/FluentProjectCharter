using Server.Database.Contracts;
using Shared.Models.BudgetItems;

namespace Server.Database.Entities.BudgetItems.Commons
{
    public class Alteration : BudgetItem
    {
        public override string Letter { get; set; } = "A";
        public string CostCenter { get; set; } = string.Empty;
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }

        public static Alteration Create(Guid ProjectId, Guid? GanttTaskId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ProjectId = ProjectId,
                //GanttTaskId = GanttTaskId,

            };
        }


    }

}
