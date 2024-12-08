using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.Database.Configurations
{
    internal class SucessfullFactorConfig : IEntityTypeConfiguration<SucessfullFactor>
    {
        public void Configure(EntityTypeBuilder<SucessfullFactor> builder)
        {
            builder.HasKey(ci => ci.Id);



        }

    }
}
