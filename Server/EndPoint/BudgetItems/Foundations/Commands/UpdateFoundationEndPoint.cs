using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Foundations.Requests;


namespace Server.EndPoint.Foundations.Commands
{
    public static class UpdateFoundationEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Foundations.EndPoint.Update, async (UpdateFoundationRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Foundation>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Foundations.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Foundation Map(this UpdateFoundationRequest request, Foundation row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;
    
            row.Quantity = request.Quantity;
            row.Budget = request.Budget;
            
            return row;
        }

    }

}
