namespace Server.Database.Entities.BudgetItems.Commons
{
    public class Testing : BudgetItem
    {

        public override string Letter { get; set; } = "N";
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }

    }

}
