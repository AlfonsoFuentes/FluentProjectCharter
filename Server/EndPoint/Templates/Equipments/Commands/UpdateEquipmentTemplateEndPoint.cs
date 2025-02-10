using Shared.Models.Templates.Equipments.Requests;
using Shared.Models.Templates.NozzleTemplates;

namespace Server.EndPoint.Templates.Equipments.Commands
{
    public static class UpdateEquipmentTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EquipmentTemplates.EndPoint.Update, async (UpdateEquipmentTemplateRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<EquipmentTemplate>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);

                    Data.Map(row);

                    await UpdateNozzles(Repository, Data);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.EquipmentTemplates.Cache.Key(row.Id));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            async Task UpdateNozzles(IRepository Repository, UpdateEquipmentTemplateRequest Data)
            {
                Expression<Func<NozzleTemplate, bool>> Criteria = x => x.TemplateId == Data.Id;

                var nozzles = await Repository.GetAllAsync(Criteria: Criteria);
                foreach (var item in nozzles)
                {
                    if (!Data.Nozzles.Any(x => x.Id == item.Id))
                    {
                        await Repository.RemoveAsync(item);
                    }
                }

                foreach (var item in Data.Nozzles)
                {
                    var nozzle = await Repository.GetByIdAsync<NozzleTemplate>(item.Id);
                    if (nozzle != null)
                    {
                        await Repository.UpdateAsync(nozzle);
                        item.Map(nozzle);
                    }
                    else
                    {
                        nozzle = NozzleTemplate.Create(Data.Id);
                        item.Map(nozzle);
                        await Repository.AddAsync(nozzle);

                    }

                }

            }
        }


        static EquipmentTemplate Map(this UpdateEquipmentTemplateRequest request, EquipmentTemplate row)
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
        static NozzleTemplate Map(this NozzleTemplateResponse request, NozzleTemplate row)
        {
            row.NominalDiameter = request.NominalDiameter.Name;
            row.ConnectionType = request.ConnectionType.Name;
            row.NozzleType = request.NozzleType.Name;
            return row;
        }
    }

}
