﻿using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Equipments
{
    public class EquipmentTemplate : Template
    {
        public string Type { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int InternalMaterial { get; set; }
        public int ExternalMaterial { get; set; }
        public double Value { get; set; }

        [ForeignKey("EquipmentTemplateId")]
        public List<Equipment> Equipments { get; set; } = new();
       
    }

}
