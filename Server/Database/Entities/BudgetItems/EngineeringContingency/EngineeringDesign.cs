namespace Server.Database.Entities.BudgetItems.EngineeringContingency
{
    public class EngineeringDesign : BudgetItem
    {

        public override string Letter { get; set; } = "OD";

        public static EngineeringDesign Create(Guid ProjectId, Guid? GanttTaskId)
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
