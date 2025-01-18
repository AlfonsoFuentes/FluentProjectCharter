using Server.EndPoint.InstrumentTemplates.Queries;
using Shared.Models.Templates.Instruments.Requests;

namespace Server.EndPoint.InstrumentTemplates.Commands
{

    public static class CreateInstrumentTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.InstrumentTemplates.EndPoint.Create, async (CreateInstrumentTemplateRequest Data, IRepository Repository) =>
                {
                    var row = Template.AddInstrumentTemplate();

                    await Repository.AddAsync(row);

                    Data.Map(row);
                  
                    var response = row.Map();
                    foreach (var nozzle in Data.Nozzles)
                    {

                        var nozzleTemplate = NozzleTemplate.Create(row.Id);
                        nozzleTemplate.NominalDiameter = nozzle.NominalDiameter.Name;
                        nozzleTemplate.ConnectionType = nozzle.ConnectionType.Name;
                        nozzleTemplate.NozzleType = nozzle.NozzleType.Name;
                        await Repository.AddAsync(nozzleTemplate);
                    }
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.InstrumentTemplates.Cache.Key(row.Id));

                    return Result.EndPointResult(response, result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static InstrumentTemplate Map(this CreateInstrumentTemplateRequest request, InstrumentTemplate row)
        {
            row.Value = request.Value;
            row.TagLetter = request.TagLetter;
            row.Reference = request.Reference;
            row.Material = request.Material.Name;

            row.Model = request.Model;
            row.BrandTemplateId = request.BrandResponse!.Id;
            row.SubType = request.SubType.Name;
            row.Type = request.Type.Name;
            row.SignalType = request.SignalType.Name;


            return row;
        }

    }

}
