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
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
     
        public string Area { get; set; } = string.Empty;

        [ForeignKey("ExpertId")]
        public List<ExpertJudgement> Judgements { get; set; } = new();
        public static StakeHolder Create()
        {
            return new()
            {
                Id = Guid.NewGuid(),

            };
        }
        public RoleInsideProject? RoleInsideProject { get; set; } = null!;
        public Guid? RoleInsideProjectId {  get; set; }
    }
}
