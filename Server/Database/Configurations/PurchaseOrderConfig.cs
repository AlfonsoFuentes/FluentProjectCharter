using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Database.Entities.PurchaseOrders;

namespace Server.Database.Configurations
{
    internal class PurchaseOrderConfig : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasOne(c => c.Deliverable).WithMany(t => t.PurchaseOrders).HasForeignKey(x => x.DeliverableId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Supplier).WithMany(t => t.PurchaseOrders).HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.PurchaseOrderItems).WithOne(t => t.PurchaseOrder).HasForeignKey(e => e.PurchaseOrderId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }

    }
}
