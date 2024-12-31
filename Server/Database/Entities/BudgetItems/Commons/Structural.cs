namespace Server.Database.Entities.BudgetItems.Commons
{
    public class Structural : BudgetItem
    {

        public override string Letter { get; set; } = "C";
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }

    }

}
