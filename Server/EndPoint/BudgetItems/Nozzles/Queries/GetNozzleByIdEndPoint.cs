using Server.Database.Entities.BudgetItems.Commons;
using Server.EndPoint.Projects.Queries;
using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.NozzleTypes;
using Shared.Models.BudgetItems.Nozzles.Records;
using Shared.Models.BudgetItems.Nozzles.Responses;


namespace Server.EndPoint.Nozzles.Queries
{
    public static class GetNozzleByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Nozzles.EndPoint.GetById, async (GetNozzleByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Nozzle>, IIncludableQueryable<Nozzle, object>> Includes = x => x
                    .Include(x => x.EngineeringItem)
                    .Include(x => x.ItemConnected!)
                    ;

                    Expression<Func<Nozzle, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Nozzles.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria, Includes: Includes);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map();
                    return Result.Success(response);

                });
            }
        }
        public static NozzleResponse Map(this Nozzle row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                ConnectionType = ConnectionTypeEnum.GetType(row.ConnectionType),
                NozzleType = NozzleTypeEnum.GetType(row.NozzleType),
                NominalDiameter = NominalDiameterEnum.GetType(row.NominalDiameter),
                OuterDiameter = string.IsNullOrEmpty(row.OuterDiameterUnit) ? new() : new Length(row.OuterDiameter, row.OuterDiameterUnit),
                InnerDiameter = string.IsNullOrEmpty(row.InnerDiameterUnit) ? new() : new Length(row.InnerDiameter, row.InnerDiameterUnit),
                Thickness = string.IsNullOrEmpty(row.ThicknessUnit) ? new() : new Length(row.Thickness, row.ThicknessUnit),
                Height = string.IsNullOrEmpty(row.HeightUnit) ? new() : new Length(row.Height, row.HeightUnit),
                Order = row.Order,


            };
        }


    }
}
