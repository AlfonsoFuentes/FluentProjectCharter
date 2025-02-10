using Server.EndPoint.Templates.Pipes.Commands;
using Server.EndPoint.Templates.Pipes.Queries;
using Shared.Models.Templates.Pipings.Requests;

namespace Server.EndPoint.Templates.Pipes.Commands
{

    public static class CreatePipeTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.PipeTemplates.EndPoint.Create, async (CreatePipeTemplateRequest Data, IRepository Repository) =>
                {
                    var row = Template.AddPipeTemplate();

                    await Repository.AddAsync(row);

                    Data.Map(row);

                    var response = row.Map();

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.PipeTemplates.Cache.Key(row.Id));

                    return Result.EndPointResult(response, result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static PipeTemplate Map(this CreatePipeTemplateRequest request, PipeTemplate row)
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
