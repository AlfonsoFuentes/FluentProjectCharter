using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Database.Configurations
{
    internal class HighLevelRequirementConfig : IEntityTypeConfiguration<HighLevelRequirement>
    {
        public void Configure(EntityTypeBuilder<HighLevelRequirement> builder)
        {
            builder.HasKey(ci => ci.Id);



        }

    }
}
