


using Server.Database.Entities;
using Server.EndPoint.EquipmentTemplates.Queries;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.Templates.Equipments.Requests;
using System.Threading;

namespace Server.EndPoint.EquipmentTemplates.Commands
{

    public static class CreateEquipmentTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EquipmentTemplates.EndPoint.Create, async (CreateEquipmentTemplateRequest Data, IRepository Repository) =>
                {
                    var row = Template.AddEquipmentTemplate();

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
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.EquipmentTemplates.Cache.Key(row.Id));

                    return Result.EndPointResult(response, result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static EquipmentTemplate Map(this CreateEquipmentTemplateRequest request, EquipmentTemplate row)
        {
            row.Value = request.Value;
            row.TagLetter = request.TagLetter;
            row.Reference = request.Reference;
            row.InternalMaterial = request.InternalMaterial.Name;
            row.ExternalMaterial = request.ExternalMaterial.Name;
            row.Model = request.Model;
            row.BrandTemplateId = request.BrandResponse!.Id;
            row.SubType = request.SubType;
            row.Type = request.Type;
            

            return row;
        }

    }

}
