using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Pipings
{
    public class EngineeringFluidCode : AuditableEntity<Guid>, ITenantCommon
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        [ForeignKey("FluidCodeId")]
        public ICollection<Isometric> Isometrics { get; set; } = new List<Isometric>();

        public static EngineeringFluidCode Create()
        {
            return new EngineeringFluidCode()
            {
                Id = Guid.NewGuid(),
            };
        }
    }

}
