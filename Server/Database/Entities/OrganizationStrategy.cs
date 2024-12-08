using Server.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities
{
    public class OrganizationStrategy : AuditableEntity<Guid>, ITenantCommon
    {
        public string Name { get; set; } = string.Empty;
        [ForeignKey("OrganizationStrategyId")]
        public List<Case> Cases { get; set; } = new();
        public static OrganizationStrategy Create()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                
            };
        }
    }
}
