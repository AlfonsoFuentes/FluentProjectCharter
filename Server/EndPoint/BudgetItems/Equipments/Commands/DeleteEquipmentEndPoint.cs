using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Equipments.Requests;

namespace Server.EndPoint.Equipments.Commands
{
    public static class DeleteEquipmentEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Equipments.EndPoint.Delete, async (DeleteEquipmentRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Equipment>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Equipments.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
