using Shared.Enums.CurrencyEnums;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.PurchaseOrders.Requests
{
    public class ApprovePurchaseOrderRequest : UpdateMessageResponse, IRequest
    {

       
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public DateTime ExpectedDate { get; set; }
        public string EndPointName => StaticClass.PurchaseOrders.EndPoint.Approve;

        public override string Legend => Name;

        public override string ClassName => StaticClass.PurchaseOrders.ClassName;
    }
}
