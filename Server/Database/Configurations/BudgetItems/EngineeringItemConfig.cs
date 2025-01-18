using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;


namespace Server.Database.Configurations.BudgetItems
{
    internal class EngineeringItemConfig : IEntityTypeConfiguration<EngineeringItem>
    {
        public void Configure(EntityTypeBuilder<EngineeringItem> builder)
        {
            builder.HasMany(x => x.Nozzles)
              .WithOne(t => t.EngineeringItem)
              .HasForeignKey(e => e.EngineeringItemId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

        //    builder.HasOne(x => x.ProcessFlowDiagram)
        //.WithMany(t => t.EngineeringItems)
        //.HasForeignKey(e => e.ProcessFlowDiagramId)

        //.OnDelete(DeleteBehavior.NoAction);


        }
    }
}
