


using Server.Database.Entities;
using Server.EndPoint.Temporarys.Queries;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.Temporarys.Requests;
using System.Threading;

namespace Server.EndPoint.Temporarys.Commands
{

    public static class CreateTemporaryEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Temporarys.EndPoint.Create, async (CreateTemporaryRequest Data, IRepository Repository) =>
                {
                    var row = Temporary.Create();

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Temporarys.Cache.Key(row.Id));

                    var response = row.Map();
                    return Result.EndPointResult(response,result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Temporary Map(this CreateTemporaryRequest request, Temporary row)
        {
           row.Type = request.Type;
            row.SubType = request.SubType;
            row.BrandTemplateId = request.BrandTemplateId;
            row.EquipmentId = request.EquipmentId;
            row.EquipmentTemplateId = request.EquipmentTemplateId;
            row.ValveId = request.ValveId;
            row.ValveTemplateId = request.ValveTemplateId;
            row.InstrumentId = request.InstrumentId;
            row.InstrumentTemplateId = request.InstrumentTemplateId;
            row.PipingId = request.PipingId;
            row.PipingTemplateId = request.PipingTemplateId;
            row.TagLetter = request.TagLetter;
            row.Reference = request.Reference;
            row.Model = request.Model;
            row.InternalMaterial = request.InternalMaterial.Name;
            row.ExternalMaterial = request.ExternalMaterial.Name;
            row.Material = request.Material.Name;
            row.Value = request.Value;
            row.Equipment = request.Equipment;
            row.EquipmentTemplate = request.EquipmentTemplate;
            

            return row;
        }

    }

}
