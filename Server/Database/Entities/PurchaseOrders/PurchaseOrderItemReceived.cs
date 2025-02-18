﻿using Shared.Enums.CurrencyEnums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.PurchaseOrders
{
    public class PurchaseOrderItemReceived : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public PurchaseOrderItem PurchaseOrderItem { get; set; } = null!;
        public Guid PurchaseOrderItemId { get; set; }

        public static PurchaseOrderItemReceived Create(Guid purchaseorderitemId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                PurchaseOrderItemId = purchaseorderitemId,

            };
        }

        public double ValueReceivedCurrency { get; set; }
        public double USDCOP { get; set; }
        public double USDEUR { get; set; }

        public DateTime? CurrencyDate { get; set; }
        [NotMapped]
        public string BudgetItemNomenclatoreName => PurchaseOrderItem == null ? string.Empty : PurchaseOrderItem.NomenclatoreName;
        [NotMapped]
        public CurrencyEnum PurchaseOrderCurrency => PurchaseOrderItem == null ? CurrencyEnum.None : PurchaseOrderItem.PurchaseOrderCurrency;

        [NotMapped]
        public double ValueReceivedUSD =>
           PurchaseOrderCurrency.Id == CurrencyEnum.USD.Id ? ValueReceivedCurrency :
           PurchaseOrderCurrency.Id == CurrencyEnum.COP.Id ? ValueReceivedCurrency / USDCOP :
           PurchaseOrderCurrency.Id == CurrencyEnum.EUR.Id ? ValueReceivedCurrency / USDEUR :
             0;
    }
}
