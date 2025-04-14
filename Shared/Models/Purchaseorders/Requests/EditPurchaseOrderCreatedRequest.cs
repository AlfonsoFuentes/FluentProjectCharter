using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.PurchaseOrders.Requests
{
    public class EditPurchaseOrderCreatedRequest : PurchaseOrderRequest, IRequest
    {
        public Guid PurchaseOrderId { get; set; }   
        public string EndPointName => StaticClass.PurchaseOrders.EndPoint.CreatedEdit;
        public override string Legend => Name;
        public override string ClassName => StaticClass.PurchaseOrders.ClassName;

    }
}
