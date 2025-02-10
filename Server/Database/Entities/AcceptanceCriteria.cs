﻿using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class AcceptanceCriteria : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }


        public static AcceptanceCriteria Create(Guid ProjectId, Guid? StartId, Guid? PlanningId, int Order)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                StartId = StartId,
                ProjectId = ProjectId,
                Order = Order,
                PlanningId = PlanningId,
            };
        }
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
    }
}
