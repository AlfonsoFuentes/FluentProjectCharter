using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.Database.Configurations
{
    internal class DeliverableRiskConfig : IEntityTypeConfiguration<DeliverableRisk>
    {
        public void Configure(EntityTypeBuilder<DeliverableRisk> builder)
        {
            builder.HasKey(ci => ci.Id);



        }

    }
}
