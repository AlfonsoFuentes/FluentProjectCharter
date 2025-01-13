namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams
{
    public class Temporary : AuditableEntity<Guid>, ITenantCommon
    {
        public string Type { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public Guid? BrandTemplateId { get; set; }
        public Guid? EquipmentId { get; set; }
        public Guid? EquipmentTemplateId { get; set; }

        public Guid? ValveId { get; set; }
        public Guid? ValveTemplateId { get; set; }
        public Guid? InstrumentId { get; set; }
        public Guid? InstrumentTemplateId { get; set; }

        public Guid? PipingId { get; set; }
        public Guid? PipingTemplateId { get; set; }


        public string TagLetter { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string InternalMaterial { get; set; } = string.Empty;
        public string ExternalMaterial { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public double Value { get; set; }
        public bool Equipment { get; set; }
        public bool EquipmentTemplate { get; set; }

        public static Temporary Create()
        {
            return new Temporary()
            {
                Id = Guid.NewGuid(),

            };
        }


    }

}
