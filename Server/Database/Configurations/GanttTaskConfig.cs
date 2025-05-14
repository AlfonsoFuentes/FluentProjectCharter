using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Database.Entities.ProjectManagements;


namespace Server.Database.Configurations
{
    //internal class GanttTaskConfig : IEntityTypeConfiguration<GanttTask>
    //{
    //    public void Configure(EntityTypeBuilder<GanttTask> builder)
    //    {
    //        builder.HasKey(ci => ci.Id);

    //        // Configurar la relación padre-hijo
    //        builder
    //            .HasOne(m => m.Parent) // Un hito tiene un padre
    //            .WithMany(m => m.SubTasks) // Un padre puede tener muchos subhitos
    //            .HasForeignKey(m => m.ParentId) // Clave foránea
    //            .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada para evitar problemas


    //        builder
    //          .HasOne(m => m.Dependant) // Un hito tiene un padre
    //          .WithMany(m => m.Dependants) // Un padre puede tener muchos subhitos
    //          .HasForeignKey(m => m.DependentantId) // Clave foránea
    //          .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada para evitar problemas

    //        builder
    //         .HasMany(m => m.BudgetItems) // Un hito tiene un padre
    //         .WithOne(m => m.GanttTask) // Un padre puede tener muchos subhitos
    //         .HasForeignKey(m => m.GanttTaskId) // Clave foránea
    //         .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada para evitar problemas

            

    //        builder
    //      .HasMany(m => m.DeliverableResources) // Un hito tiene un padre
    //      .WithOne(m => m.GanttTask) // Un padre puede tener muchos subhitos
    //      .HasForeignKey(m => m.GanttTaskId) // Clave foránea
    //      .OnDelete(DeleteBehavior.Cascade); //  eliminación en cascada 
    //    }

    //}
}
