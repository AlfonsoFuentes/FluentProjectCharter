﻿using DocumentFormat.OpenXml.Drawing.Diagrams;
using Server.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities
{

    public class Objective : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;

        public static Objective Create(Guid ProjectId, Guid? StartId, Guid? PlanningId, int Order)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ProjectId = ProjectId,
                Order = Order,
                StartId = StartId,
                PlanningId = PlanningId,
            };
        }

        public Guid? StartId { get; set; }

        public Guid? PlanningId { get; set; }

    }
}
