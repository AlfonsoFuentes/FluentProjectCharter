using Shared.Enums.CurrencyEnums;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Suppliers.Requests
{
    public class UpdateSupplierRequest : UpdateMessageResponse, IRequest
    {

       
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string VendorCode { get; set; } = string.Empty;
        public string TaxCodeLD { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string TaxCodeLP { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; } = string.Empty;
        public string? ContactName { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? ContactEmail { get; set; } = string.Empty;
        public CurrencyEnum SupplierCurrency { get; set; } = CurrencyEnum.None;
        public string EndPointName => StaticClass.Suppliers.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Suppliers.ClassName;
    }
}
