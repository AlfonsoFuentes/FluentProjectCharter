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

        public double Budget => BudgetCalculated;
        public double Percentage { get; set; }
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
        public List<TaxItemResponse> TaxItems { get; set; } = new List<TaxItemResponse>();
        public double BudgetTaxItem => TaxItems.Count == 0 ? 0 : TaxItems.Sum(x => x.Budget);

        double BudgetCalculated => BudgetTaxItem * Percentage / 100;
    }
}
