using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Instruments
{
    public class Instrument : EngineeringItem
    {


        public override string Letter { get; set; } = "G";

        public string SerialNumber { get; set; } = string.Empty;

        public InstrumentTemplate? InstrumentTemplate { get; set; } = null!;
        public Guid? InstrumentTemplateId { get; set; }
    }

}
