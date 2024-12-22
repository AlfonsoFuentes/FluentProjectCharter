using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.Interfaces.Database
{
    public interface IAppDbContext
    {
        string _tenantId { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbSet<Project> Projects { get; set; }
        DbSet<Case> Cases { get; set; }
        DbSet<BackGround> BackGrounds { get; set; }
        DbSet<StakeHolder> StakeHolders { get; set; }
        DbSet<Scope> Scopes { get; set; }
        DbSet<OrganizationStrategy> OrganizationStrategies { get; set; }
        DbSet<KnownRisk> KnownRisks { get; set; }
        DbSet<SucessfullFactor> SucessfullFactors { get; set; }
        DbSet<DecissionCriteria> DecissionCriterias { get; set; }
        DbSet<Deliverable> Deliverables { get; set; }
        DbSet<Requirement> Requirements { get; set; }
        DbSet<Assumption> Assumptions { get; set; }
        DbSet<DeliverableRisk> DeliverableRisks { get; set; }
        DbSet<Constrainst> Constrainsts { get; set; }
        DbSet<Bennefit> Bennefits { get; set; }
        DbSet<ExpertJudgement> ExpertJudgements { get; set; }
        DbSet<HighLevelRequirement> HighLevelRequirements { get; set; }
        DbSet<AppState> AppStates { get; set; }     
        DbSet<RoleInsideProject> RoleInsideProjects { get; set; }
        Task<int> SaveChangesAndRemoveCacheAsync(params string[] cacheKeys);
        Task<T> GetOrAddCacheAsync<T>(string key, Func<Task<T>> addItemFactory);
    }
}
