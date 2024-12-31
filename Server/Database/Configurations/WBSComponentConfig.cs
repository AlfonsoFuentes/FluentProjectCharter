using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Database.Configurations
{
    internal class WBSComponentConfig : IEntityTypeConfiguration<WBSComponent>
    {
        public void Configure(EntityTypeBuilder<WBSComponent> builder)
        {
            builder.HasOne(c => c.SubComponentRelation)
             .WithMany(t => t.SubComponents)
             .HasForeignKey(x => x.SubComponentRelationId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
