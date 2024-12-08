using Shared.Models.OrganizationStrategies.Requests;

namespace Server.EndPoint.OrganizationStrategys.Commands
{
    public static class UpdateOrganizationStrategyEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.OrganizationStrategys.EndPoint.Update, async (UpdateOrganizationStrategyRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<OrganizationStrategy>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.OrganizationStrategys.Cache.Key(row.Id));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static OrganizationStrategy Map(this UpdateOrganizationStrategyRequest request, OrganizationStrategy row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
