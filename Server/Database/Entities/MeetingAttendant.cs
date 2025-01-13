using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class MeetingAttendant : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;

        public StakeHolder? StakeHolder { get; set; } 
        public Guid ? StakeHolderId {  get; set; }
        public Meeting Meeting { get; set; } = null!;
        public Guid MeetingId { get; set; }

       
        public static MeetingAttendant Create(Guid MeetingId,Guid StakeholderId)
        {
            return new MeetingAttendant()
            {
                Id = Guid.NewGuid(),
                MeetingId = MeetingId,
                StakeHolderId = StakeholderId,
            };
        }
    }
}
