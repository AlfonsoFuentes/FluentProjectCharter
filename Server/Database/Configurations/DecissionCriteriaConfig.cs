using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.Database.Configurations
{
    internal class DecissionCriteriaConfig : IEntityTypeConfiguration<DecissionCriteria>
    {
        public void Configure(EntityTypeBuilder<DecissionCriteria> builder)
        {
            builder.HasKey(ci => ci.Id);



        }

    }
}
