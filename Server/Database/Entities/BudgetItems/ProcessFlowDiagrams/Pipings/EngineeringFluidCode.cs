using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Pipings
{
    public class EngineeringFluidCode : AuditableEntity<Guid>, ITenantCommon
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        [ForeignKey("FluidCodeId")]
        public ICollection<Pipe> Isometrics { get; set; } = new List<Pipe>();

        public static EngineeringFluidCode Create()
        {
            return new EngineeringFluidCode()
            {
                Id = Guid.NewGuid(),
            };
        }
    }

}
