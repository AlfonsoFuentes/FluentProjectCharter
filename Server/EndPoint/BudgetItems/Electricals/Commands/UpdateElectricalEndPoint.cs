using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Electricals.Requests;


namespace Server.EndPoint.Electricals.Commands
{
    public static class UpdateElectricalEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Electricals.EndPoint.Update, async (UpdateElectricalRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Electrical>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Electricals.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
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
