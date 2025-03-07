using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Pipings
{
    public class Pipe : EngineeringItem
    {
        [NotMapped]
        public new string Tag => PipeTemplate == null ? string.Empty : $"{Diameter}-{FluidCodeCode}-{TagNumber}-{Material}-{InsulationCode}";
        public override string Letter { get; set; } = "F";

       

        public EngineeringFluidCode? FluidCode { get; set; } = null!;
        public Guid? FluidCodeId { get; set; }


        [NotMapped]
        public string InsulationCode => Insulation ? "1" : "0";
        public List<IsometricItem> IsometricItems { get; set; } = new List<IsometricItem>();
        public static Pipe Create(Guid ProjectId, Guid? GanttTaskId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ProjectId = ProjectId,
                GanttTaskId = GanttTaskId,

            };
        }
        public PipeTemplate? PipeTemplate { get; set; } = null!;
        public Guid? PipeTemplateId { get; set; }

        public string FluidCodeCode { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public bool Insulation { get; set; }
        public string Diameter { get; set; } = string.Empty;
        public double MaterialQuantity { get; set; }
        public double LaborDayPrice { get; set; }
        public double EquivalentLenghPrice { get; set; }
        public double LaborQuantity { get; set; }
    }


}
