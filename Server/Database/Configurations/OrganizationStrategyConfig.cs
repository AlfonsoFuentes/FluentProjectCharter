using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.Database.Configurations
{
    internal class OrganizationStrategyConfig : IEntityTypeConfiguration<OrganizationStrategy>
    {
        public void Configure(EntityTypeBuilder<OrganizationStrategy> builder)
        {
            builder.HasKey(ci => ci.Id);



        }

    }
}
