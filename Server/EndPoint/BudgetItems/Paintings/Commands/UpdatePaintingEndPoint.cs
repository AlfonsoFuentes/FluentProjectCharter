using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Paintings.Requests;


namespace Server.EndPoint.Paintings.Commands
{
    public static class UpdatePaintingEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Paintings.EndPoint.Update, async (UpdatePaintingRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Painting>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Paintings.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Painting Map(this UpdatePaintingRequest request, Painting row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;
           
            row.Quantity = request.Quantity;
            row.Budget = request.Budget;
            
            return row;
        }

    }

}
