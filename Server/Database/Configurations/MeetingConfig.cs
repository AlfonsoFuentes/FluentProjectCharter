using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Database.Configurations
{
    internal class MeetingConfig : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasMany(x => x.MeetingAttendants)
        .WithOne(t => t.Meeting)
        .HasForeignKey(e => e.MeetingId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

        }

    }
    internal class MeetingAttendantConfig : IEntityTypeConfiguration<MeetingAttendant>
    {
        public void Configure(EntityTypeBuilder<MeetingAttendant> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasMany(x => x.MeetingAttendantSuggestions)
        .WithOne(t => t.MeetingAttendant)
        .HasForeignKey(e => e.MeetingAttendantId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
