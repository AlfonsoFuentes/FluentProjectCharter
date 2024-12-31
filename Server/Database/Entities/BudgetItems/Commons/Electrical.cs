namespace Server.Database.Entities.BudgetItems.Commons
{
    public class Electrical : BudgetItem
    {

        public override string Letter { get; set; } = "E";
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }

    }

}
