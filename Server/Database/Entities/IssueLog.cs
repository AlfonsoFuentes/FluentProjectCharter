using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class IssueLog : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type {  get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public StakeHolder? StakeHolderIssuer { get; set; } = null!;
        public Guid? StakeHolderIssuerId {  get; set; }

        public StakeHolder? StakeHolderResponsible { get; set; } = null!;
        public Guid? StakeHolderResponsibleId { get; set; }

        public DateTime? IssueDate { get; set; }
        public DateTime? SolveDate { get; set; }
        public string HowToSolveIssue {  get; set; }=string.Empty;  

        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
    }


}
