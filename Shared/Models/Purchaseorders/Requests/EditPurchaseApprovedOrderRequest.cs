using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.PurchaseOrders.Requests
{
    public class EditPurchaseApprovedOrderRequest : PurchaseOrderRequest, IRequest
    {


        public string EndPointName => StaticClass.PurchaseOrders.EndPoint.EditApproved;

        public override string Legend => Name;

        public override string ClassName => StaticClass.PurchaseOrders.ClassName;
    }
}
