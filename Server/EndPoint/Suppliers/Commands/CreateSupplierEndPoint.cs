using Server.Database.Entities.PurchaseOrders;
using Shared.Models.Suppliers.Requests;

namespace Server.EndPoint.Suppliers.Commands
{

    public static class CreateSupplierEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Suppliers.EndPoint.Create, async (CreateSupplierRequest Data, IRepository Repository) =>
                {
                    var row = Supplier.Create();

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Suppliers.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Supplier Map(this CreateSupplierRequest request, Supplier row)
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
