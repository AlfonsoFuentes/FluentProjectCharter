using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class Meeting : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime? DateofMeeting { get; set; }
        public string MeetingType { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public static Meeting Create(Guid ProjectId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ProjectId = ProjectId,

            };
        }
        public ICollection<MeetingAttendant> MeetingAttendants { get; set; } = new List<MeetingAttendant>();
        public ICollection<MeetingAgreement> MeetingAgreements { get; set; } = new List<MeetingAgreement>();
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }

        public bool IsNodeOpen { get; set; }
        public string? Tab { get; set; } = string.Empty;
    }
}
