﻿using Server.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Nozzles
{
    public class Nozzle : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public EngineeringItem EngineeringItem { get; set; } = null!;
        public Guid EngineeringItemId { get; set; }
        public EngineeringItem? ItemConnected { get; set; } = null!;
        public Guid? ItemConnectedId { get; set; }
        [NotMapped]
        public string Name => $"N{Order}";
        public string ConnectionType { get; set; } = string.Empty;
        public string NominalDiameter { get; set; } = string.Empty;
        public string NozzleType { get; set; } = string.Empty;
  
        public double OuterDiameter { get; set; }
        public double InnerDiameter { get; set; }
        public double Thickness { get; set; }
        public double Height { get; set; }
        public string OuterDiameterUnit { get; set; } = string.Empty;
        public string ThicknessUnit { get; set; } = string.Empty;
        public string InnerDiameterUnit { get; set; } = string.Empty;
        public string HeightUnit { get; set; } = string.Empty;
        public int Order { get; set; }

        public static Nozzle Create(Guid EngineeringItemId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                EngineeringItemId = EngineeringItemId,
            };
        }

    }

}
