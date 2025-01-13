using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Enums;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams
{
    public abstract class Template : AuditableEntity<Guid>, ITenantCommon
    {
        public string Type { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public Brand? BrandTemplate { get; set; } = null!;
        public Guid? BrandTemplateId { get; set; }
        public string TagLetter { get; set; } = string.Empty;
        public string Brand => BrandTemplate == null ? string.Empty : BrandTemplate.Name;
 
        public List<NozzleTemplate> NozzleTemplates { get; set; } = new();

        public static EquipmentTemplate AddEquipmentTemplate()
        {
            return new()
            {
                Id = Guid.NewGuid(),

            };
        }
        public static InstrumentTemplate AddInstrumentTemplate()
        {
            return new()
            {
                Id = Guid.NewGuid(),

            };
        }
        public static ValveTemplate AddValveTemplate()
        {
            return new()
            {
                Id = Guid.NewGuid(),

            };
        }
        public static PipeTemplate AddPipeTemplate()
        {
            return new()
            {
                Id = Guid.NewGuid(),

            };
        }
    }

}
