using Shared.Enums.CurrencyEnums;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.PurchaseOrders.Requests
{
    public class ApprovePurchaseOrderRequest : PurchaseOrderRequest, IRequest
    {

        
        public string EndPointName => StaticClass.PurchaseOrders.EndPoint.Approve;

        public override string Legend => Name;

        public override string ClassName => StaticClass.PurchaseOrders.ClassName;
    }
}
