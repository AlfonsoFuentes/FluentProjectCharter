


using Server.Database.Entities;
using Server.EndPoint.OrganizationStrategys.Queries;
using Shared.Models.OrganizationStrategies.Requests;
using System.Threading;

namespace Server.EndPoint.OrganizationStrategys.Commands
{

    public static class CreateOrganizationStrategyEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.OrganizationStrategys.EndPoint.Create, async (CreateOrganizationStrategyRequest Data, IRepository Repository) =>
                {
                    var row = OrganizationStrategy.Create();

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.OrganizationStrategys.Cache.Key(row.Id));

                    var response = row.Map();
                    return Result.EndPointResult(response,result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static OrganizationStrategy Map(this CreateOrganizationStrategyRequest request, OrganizationStrategy row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
