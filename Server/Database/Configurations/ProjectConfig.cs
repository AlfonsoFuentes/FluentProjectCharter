using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

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

        }

    }
}
