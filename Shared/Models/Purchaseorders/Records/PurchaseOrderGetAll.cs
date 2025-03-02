using Shared.Models.FileResults.Generics.Records;

namespace Shared.Models.PurchaseOrders.Records
{
    public class PurchaseOrderGetAll : IGetAll
    {
        public string EndPointName => StaticClass.PurchaseOrders.EndPoint.GetAll;
    }
}
