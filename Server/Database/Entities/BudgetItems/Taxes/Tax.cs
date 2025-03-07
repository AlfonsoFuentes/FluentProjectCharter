namespace Server.Database.Entities.BudgetItems.Taxes
{
    public class Tax : BudgetItem
    {

        public override string Letter { get; set; } = "L";
        public double Percentage { get; set; }
        public ICollection<TaxesItem> TaxesItems { get; set; } = new List<TaxesItem>();

        public static Tax Create(Guid ProjectId, Guid? GanttTaskId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ProjectId = ProjectId,
                GanttTaskId = GanttTaskId,

            };
        }
    }

}
