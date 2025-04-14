using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Valves;


namespace Server.Database.Configurations.BudgetItems
{
    internal class ValveConfig : IEntityTypeConfiguration<Valve>
    {
        public void Configure(EntityTypeBuilder<Valve> builder)
        {
            builder.HasOne(x => x.ValveTemplate)
                 .WithMany(t => t.Valves)
                 .HasForeignKey(e => e.ValveTemplateId)

         .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
