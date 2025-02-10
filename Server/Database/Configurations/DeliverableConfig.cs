using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Server.Database.Configurations
{
    internal class DeliverableConfig : IEntityTypeConfiguration<Deliverable>
    {
        public void Configure(EntityTypeBuilder<Deliverable> builder)
        {
            builder.HasKey(ci => ci.Id);



            //builder.HasMany(x => x.Milestones)
            //    .WithOne(t => t.Deliverable)
            //    .HasForeignKey(e => e.DeliverableId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);



        }

    }
}
