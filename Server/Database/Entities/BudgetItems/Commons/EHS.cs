namespace Server.Database.Entities.BudgetItems.Commons
{
    public class EHS : BudgetItem
    {

        public override string Letter { get; set; } = "K";
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }

    }

}
