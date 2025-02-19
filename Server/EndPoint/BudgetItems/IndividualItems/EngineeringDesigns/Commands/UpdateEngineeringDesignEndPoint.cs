using DocumentFormat.OpenXml.Spreadsheet;
using Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Requests;


namespace Server.EndPoint.BudgetItems.IndividualItems.EngineeringDesigns.Commands
{
    public static class UpdateEngineeringDesignEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EngineeringDesigns.EndPoint.Update, async (UpdateEngineeringDesignRequest data, IRepository repository) =>
                {
                    var row = await repository.GetByIdAsync<EngineeringDesign>(data.Id);
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


        static EngineeringDesign Map(this UpdateEngineeringDesignRequest request, EngineeringDesign row)
        {
            row.Name = request.Name;

            row.Budget = request.Budget;

            return row;
        }

    }

}
