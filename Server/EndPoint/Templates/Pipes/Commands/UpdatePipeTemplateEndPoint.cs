using Shared.Models.Templates.Pipings.Requests;

namespace Server.EndPoint.Templates.Pipes.Commands
{
    public static class UpdatePipeTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.PipeTemplates.EndPoint.Update, async (UpdatePipeTemplateRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<PipeTemplate>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);

                    Data.Map(row);


                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.PipeTemplates.Cache.Key(row.Id));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }

        }


        static PipeTemplate Map(this UpdatePipeTemplateRequest request, PipeTemplate row)
        {
            row.BrandTemplateId = request.BrandResponse!.Id;
            row.Diameter = request.Diameter.Name;
            row.Class = request.Class.Name;
            row.EquivalentLenghPrice = request.EquivalentLenghPrice;
            row.LaborDayPrice = request.LaborDayPrice;
            row.Insulation = request.Insulation;
            row.Material = request.Material.Name;


            return row;
        }

    }

}
