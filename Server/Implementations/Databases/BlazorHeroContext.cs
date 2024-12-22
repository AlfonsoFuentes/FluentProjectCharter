



using LazyCache;
using Microsoft.EntityFrameworkCore;
using Server.Database.Contracts;
using Server.Database.Entities;
using Server.Interfaces.Database;
using Server.Interfaces.UserServices;
using System.Reflection;

namespace Server.Implementations.Databases
{
    public class BlazorHeroContext : AuditableContext, IAppDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        private readonly IAppCache _cache;
        public string _tenantId { get; set; }

        public BlazorHeroContext(DbContextOptions<BlazorHeroContext> options, ICurrentUserService currentUserService, IAppCache cache)
            : base(options)
        {
            _currentUserService = currentUserService;

            _tenantId = currentUserService.Email;
            _cache = cache;
        }
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Case> Cases { get; set; } = null!;
        public DbSet<BackGround> BackGrounds { get; set; } = null!;
        public DbSet<StakeHolder> StakeHolders { get; set; } = null!;
        public DbSet<Scope> Scopes { get; set; } = null!;
        public DbSet<OrganizationStrategy> OrganizationStrategies { get; set; } = null!;
        public DbSet<KnownRisk> KnownRisks { get; set; } = null!;
        public DbSet<SucessfullFactor> SucessfullFactors { get; set; } = null!;
        public DbSet<DecissionCriteria> DecissionCriterias { get; set; } = null!;
        public DbSet<Deliverable> Deliverables { get; set; } = null!;
        public DbSet<Requirement> Requirements { get; set; } = null!;
        public DbSet<Assumption> Assumptions { get; set; } = null!;
        public DbSet<DeliverableRisk> DeliverableRisks { get; set; } = null!;
        public DbSet<Constrainst> Constrainsts { get; set; } = null!;
        public DbSet<Bennefit> Bennefits { get; set; } = null!;
        public DbSet<ExpertJudgement> ExpertJudgements { get; set; } = null!;
        public DbSet<HighLevelRequirement> HighLevelRequirements { get; set; } = null!;
        public DbSet<AppState> AppStates { get; set; } = null!;

        public DbSet<RoleInsideProject> RoleInsideProjects { get; set; } = null!;
        void ConfiguerQueryFilters(ModelBuilder builder)
        {

            builder.Entity<Project>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<Case>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<BackGround>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<StakeHolder>().HasQueryFilter(p => p.IsDeleted == false);
            builder.Entity<Scope>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<OrganizationStrategy>().HasQueryFilter(p => p.IsDeleted == false);
            builder.Entity<KnownRisk>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<SucessfullFactor>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<DecissionCriteria>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<Deliverable>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<Requirement>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<Assumption>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<DeliverableRisk>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<Constrainst>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<Bennefit>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<ExpertJudgement>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<HighLevelRequirement>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);
            builder.Entity<AppState>().HasQueryFilter(p => p.IsDeleted == false && EF.Property<string>(p, "TenantId") == _tenantId);

            builder.Entity<RoleInsideProject>().HasQueryFilter(p => p.IsDeleted == false);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            ConfigureDatatTypes(builder);

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            ConfiguerQueryFilters(builder);


        }

        void ConfigureDatatTypes(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
           .SelectMany(t => t.GetProperties())
           .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.Name is "LastModifiedBy" or "CreatedBy"))
            {
                property.SetColumnType("nvarchar(128)");
            }
        }

        public async Task<int> SaveChangesAndRemoveCacheAsync(params string[] cacheKeys)
        {
            var result = await SaveChangesAsync();


            foreach (var cacheKey in cacheKeys)
            {
                var key = $"{cacheKey}-{_tenantId}";
                _cache.Remove(key);
            }
            return result;
        }

        public Task<T> GetOrAddCacheAsync<T>(string key, Func<Task<T>> addItemFactory)
        {
            if (_cache == null)
            {
                throw new ArgumentNullException("cache");
            }
            key = $"{key}";
            return _cache.GetOrAddAsync(key, addItemFactory);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(_tenantId))
                {
                    return await base.SaveChangesAsync();
                }
                var AddedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is ITenantEntity);
                foreach (var item in AddedEntities)
                {
                    var entity = item.Entity as ITenantEntity;
                    entity!.TenantId = _tenantId;

                }
                var entittes = ChangeTracker.Entries<IAuditableEntity>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified).ToList();


                foreach (var row in entittes)
                {
                    if (row.State == EntityState.Added)
                    {
                        row.Entity.CreatedOn = DateTime.Now;
                        row.Entity.CreatedBy = _tenantId;

                    }

                    if (row.State == EntityState.Modified)
                    {
                        row.Entity.LastModifiedOn = DateTime.Now;
                        row.Entity.LastModifiedBy = _tenantId;
                    }

                }


                return await base.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                string exm = ex.Message;
            }

            return 0;
        }


    }
}
