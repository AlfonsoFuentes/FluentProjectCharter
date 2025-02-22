namespace Server.Database.Entities.BudgetItems.Commons
{
    public class Foundation : BudgetItem
    {

        public override string Letter { get; set; } = "B";
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }
        public static Foundation Create(Guid ProjectId, Guid? DeliverableId)
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
