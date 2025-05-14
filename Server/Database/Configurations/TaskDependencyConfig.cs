using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Server.Database.Configurations
{
    //internal class TaskDependencyConfig : IEntityTypeConfiguration<PublisherObserver>
    //{
    //    public void Configure(EntityTypeBuilder<PublisherObserver> builder)
    //    {
    //        builder.Ignore(td => td.Id);

    //        builder
    //        .HasKey(td => new { td.PublisherId, td.ObserverId });

    //        // Relación: Task -> Dependencies
    //        builder
    //            .HasOne(td => td.Publisher)
    //            .WithMany(t => t.Publishers)
    //            .HasForeignKey(td => td.PublisherId)
    //            .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada

    //        // Relación: PredecessorTask -> DependentTasks
    //        builder
    //            .HasOne(td => td.Observer)
    //            .WithMany(t => t.Observers)
    //            .HasForeignKey(td => td.ObserverId)
    //            .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada



    //    }

    //}
}
