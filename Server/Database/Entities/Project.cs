using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class Project : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;   
        public List<Case> Cases { get; set; } = new();       
        public static Project Create()
        {
            return new Project()
            {
                Id = Guid.NewGuid(),
            };
        }
       

    }


}
