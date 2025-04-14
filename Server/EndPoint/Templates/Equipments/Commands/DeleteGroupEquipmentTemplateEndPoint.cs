using Shared.Models.AcceptanceCriterias.Requests;
using Shared.Models.Templates.Equipments.Requests;

namespace Server.EndPoint.Templates.Equipments.Commands
{
    public static class DeleteGroupEquipmentTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EquipmentTemplates.EndPoint.DeleteGroup, async (DeleteGroupEquipmentTemplatesRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<EquipmentTemplate>, IIncludableQueryable<EquipmentTemplate, object>> Includes = x => x
                    .Include(x => x.Equipments);
                    foreach (var rowItem in Data.SelecteItems)
                    {
                       
                        Expression<Func<EquipmentTemplate, bool>> Criteria = x => x.Id == rowItem.Id;

                        var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                        if (row != null)
                        {
                            foreach (var item in row.Equipments)
                            {
                                item.EquipmentTemplateId = null;
                                await Repository.UpdateAsync(item);
                            }
                            await Repository.RemoveAsync(row);
                        }
                    }


                    var cache = StaticClass.EquipmentTemplates.Cache.GetAll;

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache);
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
