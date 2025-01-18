


using Server.Database.Entities;
using Server.EndPoint.EngineeringFluidCodes.Queries;
using Shared.Models.OrganizationStrategies.Requests;
using System.Threading;

namespace Server.EndPoint.EngineeringFluidCodes.Commands
{

    public static class CreateEngineeringFluidCodeEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EngineeringFluidCodes.EndPoint.Create, async (CreateEngineeringFluidCodeRequest Data, IRepository Repository) =>
                {
                    var row = EngineeringFluidCode.Create();

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.EngineeringFluidCodes.Cache.Key(row.Id));

                    var response = row.Map();
                    return Result.EndPointResult(response,result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static EngineeringFluidCode Map(this CreateEngineeringFluidCodeRequest request, EngineeringFluidCode row)
        {
            row.Name = request.Name;
            row.Code = request.Code;
            return row;
        }

    }

}
