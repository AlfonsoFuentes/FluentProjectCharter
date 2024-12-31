namespace Server.Database.Entities.BudgetItems.EngineeringContingency
{
    public class Contingency : BudgetItem
    {

        public override string Letter { get; set; } = "P";
        public double Percentage { get; set; }


    }

}
