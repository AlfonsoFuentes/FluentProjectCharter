namespace Server.Database.Entities.BudgetItems.Commons
{
    public class Painting : BudgetItem
    {

        public override string Letter { get; set; } = "I";
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }
        public static Painting Create(Guid ProjectId, Guid DeliverableId)
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
