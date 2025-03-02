using Shared.Enums.CurrencyEnums;

namespace Shared.Models.Suppliers.Responses
{
    public class SupplierResponse : BaseResponse
    {

        public string VendorCode { get; set; } = string.Empty;
        public string TaxCodeLD { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string TaxCodeLP { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; } = string.Empty;
        public string? ContactName { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? ContactEmail { get; set; } = string.Empty;
        public CurrencyEnum SupplierCurrency { get; set; } = CurrencyEnum.None;

    }
}
