namespace Server.Database.Entities.BudgetItems.EngineeringContingency
{
    public class Engineering: BudgetItem
    {

        public override string Letter { get; set; } = "O";
        public double Percentage {  get; set; }
        public static Engineering Create(Guid ProjectId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ProjectId = ProjectId,

            };
        }

    }

}
