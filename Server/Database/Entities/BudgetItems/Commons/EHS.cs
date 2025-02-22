namespace Server.Database.Entities.BudgetItems.Commons
{
    public class EHS : BudgetItem
    {

        public override string Letter { get; set; } = "K";
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }
        public static EHS Create(Guid ProjectId, Guid? DeliverableId)
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
