using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Pipings.Requests;


namespace Server.EndPoint.Pipings.Commands
{
    public static class UpdatePipingEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipings.EndPoint.Update, async (UpdatePipingRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Isometric>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Pipings.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Isometric Map(this UpdatePipingRequest request, Isometric row)
        {
            row.Name = request.Name;
            row.MaterialUnitaryCost = request.MaterialUnitaryCost;
            row.MaterialQuantity = request.MaterialQuantity;
            row.LaborUnitaryCost = request.LaborUnitaryCost;
            row.LaborQuantity = request.LaborQuantity;
            row.Diameter = request.Diameter;
            row.Material = request.Material;
            row.Insulation = request.Insulation;
            row.FluidCodeName = request.FluidCodeName;
            row.TagNumber = request.TagNumber;
            row.Budget = request.Budget;

            return row;
        }

    }

}
