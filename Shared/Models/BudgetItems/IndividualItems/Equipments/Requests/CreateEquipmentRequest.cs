﻿using Shared.Enums.CostCenter;
using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Templates.Equipments.Responses;

namespace Shared.Models.BudgetItems.IndividualItems.Equipments.Requests
{
    public class CreateEquipmentRequest : CreateMessageResponse, IRequest
    {
        public Guid? GanttTaskId { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.Equipments.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Equipments.ClassName;

        public double Budget { get; set; }

        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);

        public string Reference { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public MaterialEnum InternalMaterial { get; set; } = MaterialEnum.None;
        public MaterialEnum ExternalMaterial { get; set; } = MaterialEnum.None;

        public string Type { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public string TagLetter { get; set; } = string.Empty;
        public BrandResponse BrandResponse { get; set; } = new();
        public string Brand => BrandResponse == null ? string.Empty : BrandResponse.Name;

        public string TagNumber { get; set; } = string.Empty;
        public string Tag => $"{TagLetter}-{TagNumber}";
        public string ProvisionalTag { get; set; } = string.Empty;
        public bool ShowDetails { get; set; } = false;
        public bool ShowProvisionalTag { get; set; } = false;
        public List<NozzleResponse> Nozzles { get; set; } = new();
        public bool IsExisting { get; set; }

    }
}
