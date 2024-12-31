namespace Server.Database.Entities.BudgetItems.Commons
{
    public class Foundation : BudgetItem
    {

        public override string Letter { get; set; } = "B";
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }

    }

}
