using Shared.Enums.CurrencyEnums;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Suppliers.Requests
{
    public class CreateSupplierRequest : CreateMessageResponse, IRequest
    {

        public string VendorCode { get; set; } = string.Empty;
        public string TaxCodeLD { get; set; } = "751545";
        public string NickName { get; set; } = string.Empty;
        public string TaxCodeLP { get; set; } = "721031";

        public string? PhoneNumber { get; set; } = string.Empty;
        public string? ContactName { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? ContactEmail { get; set; } = string.Empty;
        public CurrencyEnum SupplierCurrency { get; set; } = CurrencyEnum.None;
        public string Name { set; get; } = string.Empty;
        public string EndPointName => StaticClass.Suppliers.EndPoint.Create;
 
        public override string Legend => Name;

        public override string ClassName => StaticClass.Suppliers.ClassName;
    }
}
