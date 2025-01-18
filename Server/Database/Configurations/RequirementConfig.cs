﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.Database.Configurations
{
    internal class RequirementConfig : IEntityTypeConfiguration<Requirement>
    {
        public void Configure(EntityTypeBuilder<Requirement> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasOne(c => c.RequestedBy)
       .WithMany(t => t.RequirementRequestedBys)
       .HasForeignKey(x => x.RequestedById)
       .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Responsible)
       .WithMany(t => t.RequirementResponsibles)
       .HasForeignKey(x => x.ResponsibleId)
       .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Deliverable)
          .WithMany(t => t.Requirements)
          .HasForeignKey(e => e.DeliverableId)
          .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
