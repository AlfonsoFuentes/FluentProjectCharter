using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Database.Entities.BudgetItems;


namespace Server.Database.Configurations.BudgetItems
{
    internal class BudgetItemItemConfig : IEntityTypeConfiguration<BudgetItem>
    {
        public void Configure(EntityTypeBuilder<BudgetItem> builder)
        {


            builder.HasOne(x => x.Deliverable)
            .WithMany(t => t.BudgetItems)
            .HasForeignKey(e => e.DeliverableId)
            .OnDelete(DeleteBehavior.NoAction);

          
        }
    }
}
