using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class Requirement : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;

        public Deliverable Deliverable { get; set; } = null!;
        public Guid DeliverableId { get; set; }

        //public SubDeliverable? SubDeliverable { get; set; } = null!;
        //public Guid? SubDeliverableId { get; set; }

        public string Name { set; get; } = string.Empty;
        public string Type {  set; get; } = string.Empty;
        public StakeHolder? RequestedBy { get; set; }
        public Guid? RequestedById { get; set; }
        public StakeHolder? Responsible { get; set; }
        public Guid? ResponsibleId { get; set; }
        public DateTime? DueDate { get; set; }
        public string Priority {  set; get; } = string.Empty;
        public static Requirement Create(Guid DeliverableId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                DeliverableId= DeliverableId,
            };
        }
        //public static Requirement CreateSubDeliverable(Guid DeliverableId)
        //{
        //    return new()
        //    {
        //        Id = Guid.NewGuid(),
        //        SubDeliverableId = DeliverableId,
        //    };
        //}
    }
}
