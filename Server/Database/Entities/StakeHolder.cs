using Microsoft.Extensions.Hosting;
using Server.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities
{
    public class StakeHolder : AuditableEntity<Guid>, ITenantCommon
    {
        public List<Project> Projects { get; } = [];
        [ForeignKey("ManagerId")]
        public List<Project> Managers { get; } = [];
        [ForeignKey("SponsorId")]
        public List<Project> Sponsors { get; } = [];

        [ForeignKey("StakeHolderId")]
        public List<MeetingAttendant> MeetingAttendants { get; } = [];
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string Area { get; set; } = string.Empty;

        [ForeignKey("ExpertId")]
        public List<ExpertJudgement> Judgements { get; set; } = new();

        [ForeignKey("RequestedById")]
        public List<Requirement> RequirementRequestedBys { get; set; } = new();
        [ForeignKey("ResponsibleId")]
        public List<Requirement> RequirementResponsibles { get; set; } = new();
        public static StakeHolder CreateStart(Guid ProjectId, Guid StartId)
        {
            return new()
            {
                Id = Guid.NewGuid(),

                StartId = StartId,

            };
        }
        public static StakeHolder CreatePlanning(Guid ProjectId, Guid PlanningId)
        {
            return new()
            {
                Id = Guid.NewGuid(),



                PlanningId = PlanningId
            };
        }
       public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
        public static StakeHolder Create()
        {
            return new()
            {
                Id = Guid.NewGuid(),

            };
        }
        public RoleInsideProject? RoleInsideProject { get; set; } = null!;
        public Guid? RoleInsideProjectId { get; set; }
    }
}
