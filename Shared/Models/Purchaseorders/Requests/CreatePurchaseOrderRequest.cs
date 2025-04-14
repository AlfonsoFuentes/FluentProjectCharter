using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.PurchaseOrders.Requests
{
    public class CreatePurchaseOrderRequest : PurchaseOrderRequest, IRequest
    {
        public string EndPointName => StaticClass.PurchaseOrders.EndPoint.Create;
        public override string Legend => Name;
        public override string ClassName => StaticClass.PurchaseOrders.ClassName;

    }
}
