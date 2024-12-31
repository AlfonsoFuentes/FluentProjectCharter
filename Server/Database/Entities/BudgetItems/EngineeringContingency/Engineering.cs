namespace Server.Database.Entities.BudgetItems.EngineeringContingency
{
    public class Engineering : BudgetItem
    {

        public override string Letter { get; set; } = "O";
        public double Percentage { get; set; }


    }

}
