﻿using Shared.Enums.CostCenter;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Taxs.Requests
{
    public class UpdateTaxRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Taxs.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Taxs.ClassName;
        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;
  
        public double Budget { get; set; }

       
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
    }
}
