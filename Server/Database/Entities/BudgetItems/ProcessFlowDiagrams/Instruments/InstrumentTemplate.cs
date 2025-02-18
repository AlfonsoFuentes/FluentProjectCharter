﻿using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Instruments
{
    public class InstrumentTemplate : Template
    {
    
        public string SignalType { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;

        public string Reference { get; set; } = string.Empty;
        public double Value { get; set; }

        [ForeignKey("InstrumentTemplateId")]
        public List<Instrument> Instruments { get; set; } = new();
    }

}
