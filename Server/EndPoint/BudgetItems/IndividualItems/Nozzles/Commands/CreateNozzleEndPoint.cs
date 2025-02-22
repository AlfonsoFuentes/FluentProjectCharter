using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Requests;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Nozzles.Commands
{

    public static class CreateNozzleEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Nozzles.EndPoint.Create, async (CreateNozzleRequest Data, IRepository Repository) =>
                {


                    var row = Nozzle.Create(Data.EngineeringItemId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Nozzles.Cache.Key(row.Id), StaticClass.BudgetItems.Cache.GetAll(row.EngineeringItem.ProjectId),];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        public static Nozzle Map(this CreateNozzleRequest request, Nozzle row)
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
        public static Nozzle Map(this NozzleResponse request, Nozzle row)
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
