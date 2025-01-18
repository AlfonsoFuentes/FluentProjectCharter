using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Pipings;


namespace Server.Database.Configurations.BudgetItems.PipingTempates
{
    internal class IsometricConfig : IEntityTypeConfiguration<Pipe>
    {
        public void Configure(EntityTypeBuilder<Pipe> builder)
        {
            builder.HasMany(x => x.IsometricItems)
              .WithOne(t => t.Isometric)
              .HasForeignKey(e => e.IsometricId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(c => c.FluidCode).WithMany(t => t.Isometrics).HasForeignKey(x => x.FluidCodeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.PipeTemplate).WithMany(t => t.Isometrics).HasForeignKey(x => x.PipeTemplateId)
          .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
