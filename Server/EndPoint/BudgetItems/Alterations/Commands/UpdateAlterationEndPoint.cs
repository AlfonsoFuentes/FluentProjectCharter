using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Alterations.Requests;


namespace Server.EndPoint.Alterations.Commands
{
    public static class UpdateAlterationEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Alterations.EndPoint.Update, async (UpdateAlterationRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Alteration>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Alterations.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Alteration Map(this UpdateAlterationRequest request, Alteration row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;
            row.CostCenter=request.CostCenter.Name;
            row.Quantity = request.Quantity;
            row.Budget = request.Budget;
            
            return row;
        }

    }

}
