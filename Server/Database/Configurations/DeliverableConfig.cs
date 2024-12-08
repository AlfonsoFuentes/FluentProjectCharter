using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.Database.Configurations
{
    internal class DeliverableConfig : IEntityTypeConfiguration<Deliverable>
    {
        public void Configure(EntityTypeBuilder<Deliverable> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasMany(x => x.Requirements)
        .WithOne(t => t.Deliverable)
        .HasForeignKey(e => e.DeliverableId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Assumptions)
        .WithOne(t => t.Deliverable)
        .HasForeignKey(e => e.DeliverableId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.DeliverableRisks)
        .WithOne(t => t.Deliverable)
        .HasForeignKey(e => e.DeliverableId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Constraints)
        .WithOne(t => t.Deliverable)
        .HasForeignKey(e => e.DeliverableId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Bennefits)
    .WithOne(t => t.Deliverable)
    .HasForeignKey(e => e.DeliverableId)
    .IsRequired()
    .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
