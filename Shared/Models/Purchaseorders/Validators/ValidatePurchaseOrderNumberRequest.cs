using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.PurchaseOrders.Validators
{
   
    public class ValidatePurchaseOrderNumberRequest : ValidateMessageResponse, IRequest
    {
        public string Number { get; set; } = string.Empty;
       
        public string EndPointName => StaticClass.PurchaseOrders.EndPoint.ValidateNumber;

        public override string Legend => Number;

        public override string ClassName => StaticClass.PurchaseOrders.ClassName;
    }
   
}
