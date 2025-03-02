using Shared.Models.Suppliers.Requests;
using Shared.Models.Suppliers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Suppliers.Mappers
{
    public static class SupplierMappers
    {
        public static CreateSupplierRequest ToCreate(this SupplierResponse response)
        {
            return new()
            {
                Address = response.Address,
                ContactEmail = response.ContactEmail,
                ContactName = response.ContactName,
                Name = response.Name,
                NickName = response.NickName,
                PhoneNumber = response.PhoneNumber,
                SupplierCurrency = response.SupplierCurrency,
                TaxCodeLD = response.TaxCodeLD,
                TaxCodeLP = response.TaxCodeLP,
                VendorCode = response.VendorCode,



            };
        }
        public static UpdateSupplierRequest ToUpdate(this SupplierResponse response)
        {
            return new()
            {
                Id = response.Id,
                Address = response.Address,
                ContactEmail = response.ContactEmail,
                ContactName = response.ContactName,
                Name = response.Name,
                NickName = response.NickName,
                PhoneNumber = response.PhoneNumber,
                SupplierCurrency = response.SupplierCurrency,
                TaxCodeLD = response.TaxCodeLD,
                TaxCodeLP = response.TaxCodeLP,
                VendorCode = response.VendorCode,



            };
        }
    }
}
