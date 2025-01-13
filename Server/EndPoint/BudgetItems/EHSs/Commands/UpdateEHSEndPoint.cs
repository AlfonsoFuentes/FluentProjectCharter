using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.EHSs.Requests;


namespace Server.EndPoint.EHSs.Commands
{
    public static class UpdateEHSEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EHSs.EndPoint.Update, async (UpdateEHSRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<EHS>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.EHSs.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static EHS Map(this UpdateEHSRequest request, EHS row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;
 
            row.Quantity = request.Quantity;
            row.Budget = request.Budget;
            
            return row;
        }

    }

}
