﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Instruments
{
    public class InstrumentTemplate : Template
    {
        public int Variable { get; set; }
        public int ModifierVariable { get; set; }

        public int SignalType { get; set; }
        public int Material { get; set; }
        public int ConnectionType { get; set; }
        public string Model { get; set; } = string.Empty;
    
        public string Reference { get; set; } = string.Empty;
        public double Value { get; set; }
  
     

        [ForeignKey("InstrumentTemplateId")]
        public List<Instrument> Instruments { get; set; } = new();
    }

}
