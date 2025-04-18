using Shared.Enums.CurrencyEnums;
using Shared.Enums.PurchaseOrderStatusEnums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.PurchaseOrders
{
    public class PurchaseOrderItem : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;

        public Guid PurchaseOrderId { get; private set; }
        public PurchaseOrder PurchaseOrder { get; set; } = null!;

        public Guid? BudgetItemId { get; set; }
        public BudgetItem? BudgetItem { get; set; } = null!;
        
        public ICollection<PurchaseOrderItemReceived> PurchaseOrderReceiveds { get; set; } = new List<PurchaseOrderItemReceived>();
        public static PurchaseOrderItem Create(Guid purchasorderid, Guid mwobudgetitemid)
        {
            PurchaseOrderItem item = new PurchaseOrderItem();
            item.Id = Guid.NewGuid();
            item.BudgetItemId = mwobudgetitemid;
            item.PurchaseOrderId = purchasorderid;

            return item;
        }
        public PurchaseOrderItemReceived AddPurchaseOrderReceived()
        {
            var row = PurchaseOrderItemReceived.Create(Id);
    

            return row;
        }
        public string Name { get; set; } = string.Empty;

       
        public double UnitaryValueCurrency { get; set; }
        public double Quantity { get; set; }
        public bool IsTaxNoProductive { get; set; } = false;
        public bool IsTaxAlteration { get; set; } = false;
        [NotMapped]
        public string NomenclatoreName => BudgetItem == null ? string.Empty : BudgetItem.NomenclatoreName;
        [NotMapped]
        public CurrencyEnum PurchaseOrderCurrency => PurchaseOrder == null ? CurrencyEnum.None : PurchaseOrder.PurchaseOrderCurrencyEnum;
        [NotMapped]
        public CurrencyEnum QuoteCurrency => PurchaseOrder == null ? CurrencyEnum.None : PurchaseOrder.QuoteCurrencyEnum;
        [NotMapped]
        public double UnitaryValuePurchaseOrderCurrency => UnitaryValueCurrency;
        [NotMapped]
        public double UnitaryValueUSD => PurchaseOrderCurrency.Id == CurrencyEnum.USD.Id ? UnitaryValuePurchaseOrderCurrency :
            PurchaseOrderCurrency.Id == CurrencyEnum.COP.Id ? USDCOP == 0 ? 0 : UnitaryValuePurchaseOrderCurrency / USDCOP :
            PurchaseOrderCurrency.Id == CurrencyEnum.EUR.Id ? USDEUR == 0 ? 0 : UnitaryValuePurchaseOrderCurrency / USDEUR :
            0;
        [NotMapped]
        public double UnitaryValueQuoteCurrency => QuoteCurrency.Id == CurrencyEnum.USD.Id ? UnitaryValueUSD :
            QuoteCurrency.Id == CurrencyEnum.COP.Id ? UnitaryValueUSD * USDCOP :
            QuoteCurrency.Id == CurrencyEnum.EUR.Id ? UnitaryValueUSD * USDEUR : 0;

        [NotMapped]
        public double POItemQuoteValueCurrency => UnitaryValueQuoteCurrency * Quantity;
        [NotMapped]
        public double POItemValueUSD => UnitaryValueUSD * Quantity;

        [NotMapped]
        public double POItemActualCurrency => PurchaseOrderReceiveds == null || PurchaseOrderReceiveds.Count == 0 ? 0 : PurchaseOrderReceiveds.Sum(x => x.ValueReceivedCurrency);
        [NotMapped]
        public DateTime? CurrencyDate => PurchaseOrder == null ? null! : PurchaseOrder.CurrencyDate;
        [NotMapped]
        public DateTime? ExpectedDate => PurchaseOrder == null ? null! : PurchaseOrder.ExpectedDate;
        [NotMapped]
        public string sExpectedDate => ExpectedDate == null ? string.Empty : ExpectedDate!.Value.ToString("d");
        [NotMapped]
        public bool IsTaxEditable => PurchaseOrder == null ? false : PurchaseOrder.IsTaxEditable;
        [NotMapped]
        public bool IsCapitalizedSalary => PurchaseOrder == null ? false : PurchaseOrder.IsCapitalizedSalary;

        [NotMapped]
        public string PurchaseRequisition => PurchaseOrder == null ? string.Empty : PurchaseOrder.PurchaseRequisition;
        [NotMapped]
        public string PurchaseOrderNumber => PurchaseOrder == null ? string.Empty : PurchaseOrder.PONumber;
        [NotMapped]
        public string Supplier => PurchaseOrder == null ? string.Empty : PurchaseOrder.Supplier == null ? string.Empty : PurchaseOrder.Supplier.NickName;
        [NotMapped]
        public PurchaseOrderStatusEnum PurchaseOrderStatus => PurchaseOrder == null ? PurchaseOrderStatusEnum.None :
            PurchaseOrder.PurchaseOrderStatusEnum;
        [NotMapped]
        public double USDCOP => PurchaseOrder == null ? 0 : PurchaseOrder.USDCOP;
        [NotMapped]
        public double USDEUR => PurchaseOrder == null ? 0 : PurchaseOrder.USDEUR;

        [NotMapped]
        public double ActualUSD => PurchaseOrderReceiveds == null || PurchaseOrderReceiveds.Count == 0 ? 0 :
            PurchaseOrderReceiveds.Sum(x => x.ValueReceivedUSD);
        [NotMapped]
        public double PotentialCommitmentUSD =>
            PurchaseOrderStatus.Id == PurchaseOrderStatusEnum.Created.Id ? POItemValueUSD : 0;
        [NotMapped]
        public double ApprovedUSD =>
            PurchaseOrderStatus.Id != PurchaseOrderStatusEnum.Created.Id ? POItemValueUSD : 0;
        [NotMapped]
        public double CommitmentUSD =>
            PurchaseOrderStatus.Id == PurchaseOrderStatusEnum.Created.Id ? 0 : CommitmentCurrency <= 0 ? 0 : ApprovedUSD - ActualUSD;

        [NotMapped]
        public double CommitmentCurrency =>
           PurchaseOrderStatus.Id == PurchaseOrderStatusEnum.Created.Id ? 0 : POItemQuoteValueCurrency - POItemActualCurrency;

        [NotMapped]
        public double TotalOrderUSD => CommitmentUSD + ActualUSD;

    }
}
