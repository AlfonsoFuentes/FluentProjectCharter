﻿using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Equipments
{
    public class Equipment : EngineeringItem
    {

        public override string Letter { get; set; } = "D";

        public string SerialNumber { get; set; } = string.Empty;
        public EquipmentTemplate? EquipmentTemplate { get; set; } = null!;
        public Guid? EquipmentTemplateId { get; set; }


        public static Equipment Create(Guid ProjectId, Guid? GanttTaskId)
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
