using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Taxs.Requests;


namespace Server.EndPoint.Taxs.Commands
{
    public static class UpdateTaxEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Taxs.EndPoint.Update, async (UpdateTaxRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Tax>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Taxs.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Tax Map(this UpdateTaxRequest request, Tax row)
        {
            row.Name = request.Name;
  
            row.Budget = request.Budget;
            
            return row;
        }

    }

}
