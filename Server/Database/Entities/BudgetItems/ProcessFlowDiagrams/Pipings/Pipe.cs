using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Pipings
{
    public class Pipe : EngineeringItem
    {
        
        public override string Letter { get; set; } = "F";       

        public EngineeringFluidCode? FluidCode { get; set; } = null!;
        public Guid? FluidCodeId { get; set; }
        public List<IsometricItem> IsometricItems { get; set; } = new List<IsometricItem>();
        public static Pipe Create(Guid ProjectId, Guid? GanttTaskId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ProjectId = ProjectId,
                //GanttTaskId = GanttTaskId,

            };
        }
        public PipeTemplate? PipeTemplate { get; set; } = null!;
        public Guid? PipeTemplateId { get; set; }
        public double MaterialQuantity { get; set; }
        public double LaborQuantity { get; set; }
    }


}
