using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class ExpertJudgement : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;

        public Case Case { get; set; } = null!;
        public Guid CaseId { get; set; }
        public string Name { set; get; } = string.Empty;

        public StakeHolder? Expert {  get; set; }
        public Guid? ExpertId {  get; set; }
        public static ExpertJudgement Create(Guid _CaseId)
        {
            return new ExpertJudgement()
            {
                Id = Guid.NewGuid(),
                CaseId=_CaseId,
            };
        }
    }
}
