using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.Database.Configurations
{
    internal class ConstraintConfig : IEntityTypeConfiguration<Constrainst>
    {
        public void Configure(EntityTypeBuilder<Constrainst> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasOne(x => x.Deliverable)
          .WithMany(t => t.Constraints)
          .HasForeignKey(e => e.DeliverableId)
          .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
