using Server.ExtensionsMethods.InstrumentTemplateMapper;
using Shared.Models.Templates.Equipments.Responses;

namespace Server.EndPoint.Templates.Equipments.Commands
{
    public static class UpdateEquipmentTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EquipmentTemplates.EndPoint.Update, async (EquipmentTemplateResponse Data, IRepository Repository) =>
                {
                    Expression<Func<EquipmentTemplate, bool>> Criteria = x => x.Id == Data.Id;
                    Func<IQueryable<EquipmentTemplate>, IIncludableQueryable<EquipmentTemplate, object>> Includes = x => x
                    .Include(x => x.BrandTemplate!)
                    .Include(x => x.NozzleTemplates);
                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);

                    if (row == null) { return Result.Fail(Data.NotFound); }

                    await Repository.UpdateAsync(row);

                    Data.Map(row);

                    await NozzleMapper.UpdateNozzlesTemplate(Repository, row.NozzleTemplates, Data.Nozzles, row.Id);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.EquipmentTemplates.Cache.Key(row.Id));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
           
        }




    }

}
