using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.Database.Configurations
{
    internal class StakeHolderConfig : IEntityTypeConfiguration<StakeHolder>
    {
        public void Configure(EntityTypeBuilder<StakeHolder> builder)
        {
            builder.HasKey(ci => ci.Id);



        }

    }
}
