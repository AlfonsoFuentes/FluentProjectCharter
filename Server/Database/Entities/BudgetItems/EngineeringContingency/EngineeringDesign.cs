namespace Server.Database.Entities.BudgetItems.EngineeringContingency
{
    public class EngineeringDesign : BudgetItem
    {

        public override string Letter { get; set; } = "O";

        public static EngineeringDesign Create(Guid ProjectId, Guid? DeliverableId)
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
