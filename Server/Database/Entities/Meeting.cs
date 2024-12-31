using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class LearnedLesson : AuditableEntity<Guid>, ITenantCommon
    {
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    }
    public class Meeting : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime? DateofMeeting { get; set; }
        public string MeetingType { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;
        public static Meeting Create()
        {
            return new()
            {
                Id = Guid.NewGuid(),

            };
        }
        public ICollection<MeetingAttendant> MeetingAttendants { get; set; } = new List<MeetingAttendant>();
    }
    public class MeetingAttendant : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public Meeting Meeting { get; set; } = null!;
        public Guid MeetingId { get; set; }

        public ICollection<MeetingAttendantSuggestion> MeetingAttendantSuggestions { get; set; } = new List<MeetingAttendantSuggestion>();
    }
    public class MeetingAttendantSuggestion : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public DateTime? DateofSuggestion { get; set; }
        public MeetingAttendant MeetingAttendant { get; set; } = null!;
        public Guid MeetingAttendantId { get; set; }


    }
}
