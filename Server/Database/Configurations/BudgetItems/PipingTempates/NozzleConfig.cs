using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Nozzles;


namespace Server.Database.Configurations.BudgetItems.PipingTempates
{
    internal class NozzleConfig : IEntityTypeConfiguration<Nozzle>
    {
        public void Configure(EntityTypeBuilder<Nozzle> builder)
        {
            builder.HasOne(x => x.ItemConnected)
         .WithMany(t => t.ItemConnecteds)
         .HasForeignKey(e => e.ItemConnectedId)

         .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
