using Server.EndPoint.Templates.Valves.Commands;
using Server.EndPoint.Templates.Valves.Queries;
using Shared.Models.Templates.Valves.Requests;

namespace Server.EndPoint.Templates.Valves.Commands
{

    public static class CreateValveTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ValveTemplates.EndPoint.Create, async (CreateValveTemplateRequest Data, IRepository Repository) =>
                {
                    var row = Template.AddValveTemplate();

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
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.ValveTemplates.Cache.Key(row.Id));

                    return Result.EndPointResult(response, result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static ValveTemplate Map(this CreateValveTemplateRequest request, ValveTemplate row)
        {
            row.Value = request.Value;
            row.TagLetter = request.TagLetter;

            row.Model = request.Model;
            row.BrandTemplateId = request.BrandResponse!.Id;

            row.Type = request.Type.Name;
            row.HasFeedBack = request.HasFeedBack;
            row.Diameter = request.Diameter.Name;
            row.ActuatorType = request.ActuatorType.Name;
            row.FailType = request.FailType.Name;
            row.Material = request.Material.Name;
            row.PositionerType = request.PositionerType.Name;
            row.SignalType = request.SignalType.Name;


            return row;
        }

    }

}
