using Shared.Models.EngineeringFluidCodes.Requests;

namespace Server.EndPoint.EngineeringFluidCodes.Commands
{
    public static class UpdateEngineeringFluidCodeEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EngineeringFluidCodes.EndPoint.Update, async (UpdateEngineeringFluidCodeRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<EngineeringFluidCode>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.EngineeringFluidCodes.Cache.Key(row.Id));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static EngineeringFluidCode Map(this UpdateEngineeringFluidCodeRequest request, EngineeringFluidCode row)
        {
            row.Name = request.Name;
            row.Code = request.Code;
            return row;
        }

    }

}
