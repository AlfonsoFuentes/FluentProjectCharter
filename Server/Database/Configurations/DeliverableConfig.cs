using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Server.Database.Configurations
{
    internal class DeliverableConfig : IEntityTypeConfiguration<Deliverable>
    {
        public void Configure(EntityTypeBuilder<Deliverable> builder)
        {
            builder.HasKey(ci => ci.Id);

           
           

         
        }

    }
}
