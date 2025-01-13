



using Server.Database.Entities;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.Templates.Equipments.Requests;

namespace Server.EndPoint.EquipmentTemplates.Commands
{
    public static class DeleteEquipmentTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EquipmentTemplates.EndPoint.Delete, async (DeleteEquipmentTemplateRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<EquipmentTemplate>, IIncludableQueryable<EquipmentTemplate, object>> Includes = x => x
                    .Include(x => x.Equipments);
                    Expression<Func<EquipmentTemplate, bool>> Criteria = x =>x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria:Criteria,Includes:Includes);

                    if (row == null) { return Result.Fail(Data.NotFound); }
                    foreach (var item in row.Equipments)
                    {
                        item.EquipmentTemplateId = null;
                        await Repository.UpdateAsync(item);
                    }
                    await Repository.RemoveAsync(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.EquipmentTemplates.Cache.Key(row.Id));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
