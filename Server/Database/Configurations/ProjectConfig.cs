using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

namespace Server.Database.Configurations
{
    internal class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(ci => ci.Id);


            builder.HasMany(x => x.Cases)
          .WithOne(t => t.Project)
          .HasForeignKey(e => e.ProjectId)
          .IsRequired()
          .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.HighLevelRequirements)
         .WithOne(t => t.Project)
         .HasForeignKey(e => e.ProjectId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.StakeHolders)
                  .WithMany(e => e.Projects);

            builder.HasOne(c => c.Sponsor)
          .WithMany(t => t.Sponsors)
          .HasForeignKey(x => x.SponsorId)
          .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Manager)
         .WithMany(t => t.Managers)
         .HasForeignKey(x => x.ManagerId)
         .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
