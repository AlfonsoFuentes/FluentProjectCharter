using Shared.Models.PurchaseOrders.Requests;
using Shared.Models.PurchaseOrders.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.PurchaseOrders.Mappers
{
    public static class PurchaseOrderMappers
    {
        public static CreatePurchaseOrderRequest ToCreate(this PurchaseOrderResponse response)
        {
            return new()
            {
               



            };
        }
        public static ApprovePurchaseOrderRequest ToApprove(this PurchaseOrderResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                ExpectedDate=response.ExpectedDate!.Value,
                Number=response.PONumber,

            };
        }
    }
}
