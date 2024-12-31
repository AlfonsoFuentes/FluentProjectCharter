using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Pipings
{
    public class PipeTemplate : Template
    {
        public double EquivalentLenghPrice { get; set; }
        public string Material { get; set; } = string.Empty;
        public double LaborDayPrice { get; set; }
        public string Diameter { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
    }

}
