using Server.Database.Contracts;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Enums;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Equipments;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Instruments;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Nozzles;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Pipings;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Valves;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams
{
    public abstract class Template : AuditableEntity<Guid>, ITenantCommon
    {
        public string Type { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public EngineeringItemType EngineeringItemType { get; set; } = EngineeringItemType.None;
        public Brand? BrandTemplate { get; set; } = null!;
        public Guid? BrandTemplateId { get; set; }
        public string TagLetter { get; set; } = string.Empty;

        public List<NozzleTemplate> NozzleTemplates { get; set; } = new();

        public static EquipmentTemplate AddEquipmentTemplate()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                EngineeringItemType = EngineeringItemType.Equipment,
            };
        }
        public static InstrumentTemplate AddInstrumentTemplate()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                EngineeringItemType = EngineeringItemType.Instrument,
            };
        }
        public static ValveTemplate AddValveTemplate()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                EngineeringItemType = EngineeringItemType.Valve,
            };
        }
        public static PipeTemplate AddPipeTemplate()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                EngineeringItemType = EngineeringItemType.Isometric,
            };
        }
    }

}
