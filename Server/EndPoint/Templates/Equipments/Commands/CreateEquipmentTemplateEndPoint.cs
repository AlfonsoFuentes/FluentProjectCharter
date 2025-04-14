using Server.ExtensionsMethods.InstrumentTemplateMapper;
using Shared.Models.Templates.Equipments.Responses;
namespace Server.EndPoint.Templates.Equipments.Commands
{

    public static class CreateEquipmentTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EquipmentTemplates.EndPoint.Create, async (EquipmentTemplateResponse Data, IRepository Repository) =>
                {
                    var row = Template.AddEquipmentTemplate();

                    await Repository.AddAsync(row);

                    Data.Map(row);

                    await NozzleMapper.CreateNozzleTemplates(Repository, row.Id, Data.Nozzles);
                    
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.EquipmentTemplates.Cache.Key(row.Id));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


       

    }

}
