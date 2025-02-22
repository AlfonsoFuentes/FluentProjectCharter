using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Database.Entities.ProjectManagements;


namespace Server.Database.Configurations
{
    internal class DeliverableConfig : IEntityTypeConfiguration<Deliverable>
    {
        public void Configure(EntityTypeBuilder<Deliverable> builder)
        {
            builder.HasKey(ci => ci.Id);

            // Configurar la relación padre-hijo
            builder
                .HasOne(m => m.ParentDeliverable) // Un hito tiene un padre
                .WithMany(m => m.SubDeliverables) // Un padre puede tener muchos subhitos
                .HasForeignKey(m => m.ParentDeliverableId) // Clave foránea
                .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada para evitar problemas


            builder
              .HasOne(m => m.Dependant) // Un hito tiene un padre
              .WithMany(m => m.Dependants) // Un padre puede tener muchos subhitos
              .HasForeignKey(m => m.DependentantId) // Clave foránea
              .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada para evitar problemas

            builder
             .HasMany(m => m.BudgetItems) // Un hito tiene un padre
             .WithOne(m => m.Deliverable) // Un padre puede tener muchos subhitos
             .HasForeignKey(m => m.DeliverableId) // Clave foránea
             .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada para evitar problemas

            //builder
            // .HasMany(m => m.BudgetItemValues) // Un hito tiene un padre
            // .WithOne(m => m.Deliverable) // Un padre puede tener muchos subhitos
            // .HasForeignKey(m => m.DeliverableId) // Clave foránea
            // .OnDelete(DeleteBehavior.Cascade); // Evita la eliminación en cascada para evitar problemas

            builder
          .HasMany(m => m.DeliverableResources) // Un hito tiene un padre
          .WithOne(m => m.Deliverable) // Un padre puede tener muchos subhitos
          .HasForeignKey(m => m.DeliverableId) // Clave foránea
          .OnDelete(DeleteBehavior.Cascade); // Evita la eliminación en cascada para evitar problemas
        }

    }
}
