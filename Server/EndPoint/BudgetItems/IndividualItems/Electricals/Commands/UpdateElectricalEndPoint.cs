using DocumentFormat.OpenXml.Spreadsheet;
using Shared.Models.BudgetItems.IndividualItems.Electricals.Requests;


namespace Server.EndPoint.BudgetItems.IndividualItems.Electricals.Commands
{
    public static class UpdateElectricalEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Electricals.EndPoint.Update, async (UpdateElectricalRequest data, IRepository repository) =>
                {
                    var row = await repository.GetByIdAsync<Electrical>(data.Id);
                    if (row == null)
                    {
                        return Result.Fail(data.NotFound);
                    }

                    data.Map(row);
                    await repository.UpdateAsync(row);

                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result, data.Succesfully, data.Fail);
                });
            }
            private string[] GetCacheKeys(BudgetItem row)
            {
                List<string> cacheKeys = [
               ..StaticClass.BudgetItems.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }

        }


        static Electrical Map(this UpdateElectricalRequest request, Electrical row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;

            row.Quantity = request.Quantity;
            row.Budget = request.Budget;

            return row;
        }

    }

}
