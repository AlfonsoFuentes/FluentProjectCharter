namespace Server.Database.Entities.BudgetItems.Commons
{
    public class Testing : BudgetItem
    {

        public override string Letter { get; set; } = "N";
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }
        public static Testing Create(Guid ProjectId, Guid DeliverableId)
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
