using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Database.Configurations
{
    //internal class SubDeliverableConfig : IEntityTypeConfiguration<SubDeliverable>
    //{
    //    public void Configure(EntityTypeBuilder<SubDeliverable> builder)
    //    {
    //        builder.HasKey(ci => ci.Id);

    //        builder.HasMany(x => x.Requirements)
    //        .WithOne(t => t.SubDeliverable)
    //        .HasForeignKey(e => e.SubDeliverableId)
           
    //        .OnDelete(DeleteBehavior.NoAction);

    //        builder.HasMany(x => x.Assumptions)
    //        .WithOne(t => t.SubDeliverable)
    //        .HasForeignKey(e => e.SubDeliverableId)
           
    //        .OnDelete(DeleteBehavior.NoAction);

    //        builder.HasMany(x => x.DeliverableRisks)
    //        .WithOne(t => t.SubDeliverable)
    //        .HasForeignKey(e => e.SubDeliverableId)
          
    //        .OnDelete(DeleteBehavior.NoAction);

    //        builder.HasMany(x => x.Constraints)
    //        .WithOne(t => t.SubDeliverable)
    //        .HasForeignKey(e => e.SubDeliverableId)
         
    //        .OnDelete(DeleteBehavior.NoAction);

    //        builder.HasMany(x => x.Bennefits)
    //        .WithOne(t => t.SubDeliverable)
    //        .HasForeignKey(e => e.SubDeliverableId)
          
    //        .OnDelete(DeleteBehavior.NoAction);

    //        builder.HasMany(x => x.AcceptanceCriterias)
    //       .WithOne(t => t.SubDeliverable)
    //       .HasForeignKey(e => e.SubDeliverableId)
    //        .OnDelete(DeleteBehavior.NoAction);

           


    //    }

    //}
}
