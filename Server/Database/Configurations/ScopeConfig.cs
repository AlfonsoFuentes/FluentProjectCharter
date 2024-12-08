using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.Database.Configurations
{
    internal class ScopeConfig : IEntityTypeConfiguration<Scope>
    {
        public void Configure(EntityTypeBuilder<Scope> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasMany(x => x.Deliverables)
         .WithOne(t => t.Scope)
         .HasForeignKey(e => e.ScopeId)
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
