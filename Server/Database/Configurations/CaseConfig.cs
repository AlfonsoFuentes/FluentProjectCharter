using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.Database.Configurations
{
    internal class CaseConfig : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasMany(x => x.BackGrounds)
         .WithOne(t => t.Case)
         .HasForeignKey(e => e.CaseId)
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.Scopes)
        .WithOne(t => t.Case)
        .HasForeignKey(e => e.CaseId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.KnownRisks)
        .WithOne(t => t.Case)
        .HasForeignKey(e => e.CaseId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.SucessfullFactors)
        .WithOne(t => t.Case)
        .HasForeignKey(e => e.CaseId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.DecissionCriterias)
        .WithOne(t => t.Case)
        .HasForeignKey(e => e.CaseId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ExpertJudgements)
        .WithOne(t => t.Case)
        .HasForeignKey(e => e.CaseId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.OrganizationStrategy)
                .WithMany(t => t.Cases)
                .HasForeignKey(x => x.OrganizationStrategyId)
                .OnDelete(DeleteBehavior.NoAction);


        }

    }
}
