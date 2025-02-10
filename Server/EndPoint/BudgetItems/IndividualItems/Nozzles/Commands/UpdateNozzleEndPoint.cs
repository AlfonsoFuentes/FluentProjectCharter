using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Nozzles.Commands
{
    public static class UpdateNozzleEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Nozzles.EndPoint.Update, async (UpdateNozzleRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Nozzle>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Nozzles.Cache.Key(row.Id), StaticClass.BudgetItems.Cache.GetAll,];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Nozzle Map(this UpdateNozzleRequest request, Nozzle row)
        {
            row.Order = request.Order;
            row.ConnectionType = request.ConnectionType.Name;
            row.NominalDiameter = request.NominalDiameter.Name;
            row.InnerDiameter = request.InnerDiameter.Value;
            row.OuterDiameter = request.OuterDiameter.Value;
            row.Thickness = request.Thickness.Value;
            row.Height = request.Height.Value;
            row.OuterDiameterUnit = request.OuterDiameter.Unit.Name;
            row.ThicknessUnit = request.Thickness.Unit.Name;
            row.InnerDiameterUnit = request.InnerDiameter.Unit.Name;
            row.HeightUnit = request.Height.Unit.Name;
            row.NozzleType = request.NozzleType.Name;

            return row;
        }

    }

}
