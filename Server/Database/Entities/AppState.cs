using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class AppState : AuditableEntity<Guid>, ITenantEntity
    {
        public static AppState Create()
        {
            return new AppState()
            {
                Id = Guid.NewGuid(),
          
            };
        }
        public string TenantId { get; set; } = string.Empty;

        public Guid? ActiveProjectId { set; get; }
 

        

    }
   

}
