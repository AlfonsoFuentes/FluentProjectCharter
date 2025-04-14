using Shared.Enums.CurrencyEnums;
using Shared.Models.BudgetItems.Responses;

namespace Shared.Models.PurchaseOrders.Responses
{
    public class PurchaseOrderItemResponse : BaseResponse
    {
        public Guid? BudgetItemId { get; set; }


        public double UnitaryValueCurrency { get; set; }
        public double Quantity { get; set; }

        public CurrencyEnum PurchaseOrderCurrency { get; set; } = CurrencyEnum.None;
        public CurrencyEnum QuoteCurrency { get; set; } = CurrencyEnum.None;
        public double UnitaryValueUSD => PurchaseOrderCurrency.Id == CurrencyEnum.USD.Id ? UnitaryValueCurrency :
            PurchaseOrderCurrency.Id == CurrencyEnum.COP.Id ? USDCOP == 0 ? 0 : UnitaryValueCurrency / USDCOP :
        PurchaseOrderCurrency.Id == CurrencyEnum.EUR.Id ? USDEUR == 0 ? 0 : UnitaryValueCurrency / USDEUR :
            0;
        public double USDCOP { get; set; }
        public double USDEUR { get; set; }
        public double BudgetUSD { get; set; } = 0;
        public double TotalUSD => UnitaryValueUSD * Quantity;
        public double TotalCurrency => UnitaryValueCurrency * Quantity;
        public double ActualCurrency { get; set; } = 0;
        public double ActualUSD { get; set; }
        public double CommitmentCurrency { get; set; } = 0;
        public double CommitmentUSD { get; set; }
        public double PotentialUSD { get; set; }
        public double ApprovedUSD => ActualUSD + CommitmentUSD;
        public double AssignedUSD => ActualUSD + CommitmentUSD + PotentialUSD;
        public string sTotalCurrency => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalCurrency);
        public string sTotalUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalUSD);
        public string sActualUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", ActualUSD);
        public string sCommitmentUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", CommitmentUSD);
        public string sPotentialUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", PotentialUSD);
        public string sAssignedUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", AssignedUSD);



    }

}
