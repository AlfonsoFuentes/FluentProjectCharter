namespace Server.Database.Entities.BudgetItems.Commons
{
    public class Painting : BudgetItem
    {

        public override string Letter { get; set; } = "I";
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }

    }

}
