using Shared.Enums.CurrencyEnums;
using Shared.Enums.PurchaseOrderStatusEnums;
using Shared.Models.BudgetItems.Responses;

namespace Shared.Models.PurchaseOrders.Requests
{
    public class PurchaseOrderItemRequest
    {
        public Guid Id { get; set; }    
        public BudgetItemWithPurchaseOrdersResponse BudgetItem { get; set; } = new() { Id = Guid.Empty };
        public Guid BudgetItemId => BudgetItem == null ? Guid.Empty : BudgetItem.Id;
        public CurrencyEnum PurchaseOrderCurrency { get; set; } = CurrencyEnum.None;
        CurrencyEnum _QuoteCurrency = CurrencyEnum.None;
        public CurrencyEnum QuoteCurrency
        {
            get => _QuoteCurrency;
            set => SetQuoteCurrency(value);
        }
        public string Name { set; get; } = string.Empty;
        public string NomenclatoreName => BudgetItem == null ? string.Empty : BudgetItem.NomenclatoreName;
        public double USDCOP { get; set; }
        public double USDEUR { get; set; }
        public double Quantity { get; set; }
        public double UnitaryQuoteCurrency { get; set; }
        public double TotalQuoteCurrency => UnitaryQuoteCurrency * Quantity;
        public double UnitaryPurchaseOrderCurrency =>
            PurchaseOrderCurrency.Id == CurrencyEnum.USD.Id ? UnitaryUSD :
            PurchaseOrderCurrency.Id == CurrencyEnum.COP.Id ? UnitaryUSD * USDCOP :
             UnitaryUSD * USDEUR;
        public double TotalPurchaseOrderCurrency => UnitaryPurchaseOrderCurrency * Quantity;


        public double UnitaryUSD => QuoteCurrency.Id == CurrencyEnum.USD.Id ? UnitaryQuoteCurrency :
            QuoteCurrency.Id == CurrencyEnum.COP.Id ? USDCOP == 0 ? 0 : UnitaryQuoteCurrency / USDCOP :
            QuoteCurrency.Id == CurrencyEnum.EUR.Id ? USDEUR == 0 ? 0 : UnitaryQuoteCurrency / USDEUR :
            0;

        public double TotalUSD => UnitaryUSD * Quantity;

        public double BudgetUSD => BudgetItem == null ? 0 : BudgetItem.BudgetUSD;
        public double AssignedUSD => BudgetItem == null ? 0 : BudgetItem.AssignedUSD + TotalUSD;
        public double ToCommitUSD => BudgetUSD - AssignedUSD;

        public string sBudgetUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", BudgetUSD);
        public string sAssignedUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", AssignedUSD);
        public string sToCommitUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", ToCommitUSD);

        public void SetQuoteCurrency(CurrencyEnum _quoteCurrency)
        {
            var oldUnitaryValueUSD = UnitaryUSD;
            _QuoteCurrency = _quoteCurrency;
            UnitaryQuoteCurrency =
                _QuoteCurrency.Id == CurrencyEnum.USD.Id ? oldUnitaryValueUSD :
                _QuoteCurrency.Id == CurrencyEnum.COP.Id ? oldUnitaryValueUSD * USDCOP :
                oldUnitaryValueUSD * USDEUR;


          
        }
    }
}
