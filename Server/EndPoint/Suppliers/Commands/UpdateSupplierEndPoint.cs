using Server.Database.Entities;
using Server.Database.Entities.PurchaseOrders;
using Shared.Models.Suppliers.Requests;


namespace Server.EndPoint.Suppliers.Commands
{
    public static class UpdateSupplierEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Suppliers.EndPoint.Update, async (UpdateSupplierRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Supplier>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [ .. StaticClass.Suppliers.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static Supplier Map(this UpdateSupplierRequest request, Supplier row)
        {
            row.Name = request.Name;
            row.SupplierCurrency = request.SupplierCurrency.Id;
            row.Address = request.Address;
            row.NickName = request.NickName;
            row.VendorCode = request.VendorCode;    
            row.TaxCodeLP = request.TaxCodeLP;
            row.TaxCodeLD = request.TaxCodeLD;
            row.PhoneNumber = request.PhoneNumber;
            row.ContactEmail = request.ContactEmail;
            row.ContactName = request.ContactName;
            
            return row;
        }

    }

}
