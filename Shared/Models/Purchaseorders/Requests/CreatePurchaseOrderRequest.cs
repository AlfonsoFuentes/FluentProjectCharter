using Shared.Enums.CostCenter;
using Shared.Enums.CurrencyEnums;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.PurchaseOrders.Responses;
using Shared.Models.Suppliers.Responses;
using static Shared.Constants.Routes.AccountEndpoints;

namespace Shared.Models.PurchaseOrders.Requests
{
    public class CreatePurchaseOrderRequest : CreateMessageResponse, IRequest
    {
        public SupplierResponse Supplier { get; set; } = null!;
        public Guid? SupplierId => Supplier == null ? Guid.Empty : Supplier.Id;
        public string VendorCode => Supplier == null ? string.Empty : Supplier.VendorCode;
        public string TaxCodeLD { get; set; } = "751545";
        public string TaxCodeLP { get; set; } = "721031";
        public string QuoteNo { get; set; } = string.Empty;
        public string SPL => IsAlteration ? "0735015000" : "151605000";
        public bool IsAlteration { get; set; }
        public bool IsProductive { get; set; }
        public bool IsCapitalizedSalary { get; set; }
        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;
        public string TaxCode => IsAlteration || !IsProductive ? TaxCodeLP : TaxCodeLD;
        public DateTime? CurrencyDate { get; set; } = DateTime.UtcNow;
        public string CurrencyDateString => CurrencyDate == null ? string.Empty : CurrencyDate.Value.ToString("d");
        public string PurchaseRequisition { get; set; } = string.Empty;
        public string ProjectAccount { get; set; } = string.Empty;



        public CurrencyEnum PurchaseOrderCurrency => Supplier == null ? CurrencyEnum.COP : Supplier.SupplierCurrency;

        CurrencyEnum _QuoteCurrency = CurrencyEnum.None;
        public CurrencyEnum QuoteCurrency
        {
            get { return _QuoteCurrency; }
            set
            {
                _QuoteCurrency = value;
                foreach (var item in PurchaseOrderItems)
                {
                    item.QuoteCurrency = _QuoteCurrency;
                }

            }
        }

        double _USDCOP = 0;
        public double USDCOP
        {
            get { return _USDCOP; }
            set
            {
                _USDCOP = value;
                foreach (var item in PurchaseOrderItems)
                {
                    item.USDCOP = _USDCOP;
                }

            }
        }
        double _USDEUR = 0;
        public double USDEUR
        {
            get { return _USDEUR; }
            set
            {
                _USDEUR = value;
                foreach (var item in PurchaseOrderItems)
                {
                    item.USDEUR = _USDEUR;
                }

            }
        }
        public string Name { set; get; } = string.Empty;
        public string EndPointName => StaticClass.PurchaseOrders.EndPoint.Create;
        public override string Legend => Name;
        public override string ClassName => StaticClass.PurchaseOrders.ClassName;
        public List<CreatePurchaseOrderItemRequest> PurchaseOrderItems { get; set; } = new();
        public bool IsAnyValueNotDefined => PurchaseOrderItems.Any(x => x.TotalCurrency <= 0);
        public bool IsAnyNameEmpty => PurchaseOrderItems.Any(x => string.IsNullOrEmpty(x.Name));
        public double TotalCurrency => PurchaseOrderItems.Count == 0 ? 0 : PurchaseOrderItems.Sum(x => x.TotalCurrency);
        public double TotalUSD => PurchaseOrderItems.Count == 0 ? 0 : PurchaseOrderItems.Sum(x => x.TotalUSD);
        public string sTotalCurrency => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalCurrency);
        public string sTotalUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalUSD);
        public void AddPurchaseorderItem(BudgetItemWithPurchaseOrdersResponse BudgetItem)
        {
            CreatePurchaseOrderItemRequest item = new()
            {
                AssignedUSD = BudgetItem.AssignedUSD,
                BudgetItemId = BudgetItem.Id,
                BudgetUSD = BudgetItem.BudgetUSD,
                Name = BudgetItem.Name,
                PurchaseOrderCurrency = PurchaseOrderCurrency,
                QuoteCurrency = QuoteCurrency,
                USDCOP = USDCOP,
                USDEUR = USDEUR,
                UnitaryValueCurrency = 1000,
                Quantity = 1,

            };
            PurchaseOrderItems.Add(item);
        }
    }
    public class CreatePurchaseOrderItemRequest
    {
        public Guid BudgetItemId { get; set; }
        public CurrencyEnum PurchaseOrderCurrency { get; set; } = CurrencyEnum.None;
        public CurrencyEnum QuoteCurrency { get; set; } = CurrencyEnum.None;
        public string Name { set; get; } = string.Empty;
        public double USDCOP { get; set; }
        public double USDEUR { get; set; }
        public double Quantity { get; set; }
        public double UnitaryValueCurrency { get; set; }
        public double TotalCurrency => UnitaryValueCurrency * Quantity;
        public double UnitaryValueUSD => PurchaseOrderCurrency.Id == CurrencyEnum.USD.Id ? UnitaryValueCurrency :
           PurchaseOrderCurrency.Id == CurrencyEnum.COP.Id ? USDCOP == 0 ? 0 : UnitaryValueCurrency / USDCOP :
       PurchaseOrderCurrency.Id == CurrencyEnum.EUR.Id ? USDEUR == 0 ? 0 : UnitaryValueCurrency / USDEUR :
           0;

        public double TotalUSD => UnitaryValueUSD * Quantity;

        public double BudgetUSD { get; set; }
        public double AssignedUSD { get; set; }
        public double AvailableUSD => BudgetUSD - AssignedUSD - TotalUSD;
    }
}
