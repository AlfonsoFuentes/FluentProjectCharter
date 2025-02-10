using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Server.Database.Configurations
{
    internal class MilestoneConfig : IEntityTypeConfiguration<Milestone>
    {
        public void Configure(EntityTypeBuilder<Milestone> builder)
        {
            builder.HasKey(ci => ci.Id);


            // Configurar la relación padre-hijo
            builder
                .HasOne(m => m.ParentMilestone) // Un hito tiene un padre
                .WithMany(m => m.SubMilestones) // Un padre puede tener muchos subhitos
                .HasForeignKey(m => m.ParentMilestoneId) // Clave foránea
                .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada para evitar problemas


            builder
              .HasOne(m => m.Dependant) // Un hito tiene un padre
              .WithMany(m => m.Dependants) // Un padre puede tener muchos subhitos
              .HasForeignKey(m => m.DependentantId) // Clave foránea
              .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada para evitar problemas
        }

    }
}
