﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.Database.Configurations
{
    internal class AssumptionConfig : IEntityTypeConfiguration<Assumption>
    {
        public void Configure(EntityTypeBuilder<Assumption> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasOne(x => x.Deliverable)
           .WithMany(t => t.Assumptions)
           .HasForeignKey(e => e.DeliverableId)
           .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
