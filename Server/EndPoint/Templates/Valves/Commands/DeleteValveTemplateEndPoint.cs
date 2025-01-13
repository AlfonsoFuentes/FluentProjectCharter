



using Server.Database.Entities;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.Templates.Valves.Requests;

namespace Server.EndPoint.ValveTemplates.Commands
{
    public static class DeleteValveTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ValveTemplates.EndPoint.Delete, async (DeleteValveTemplateRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<ValveTemplate>, IIncludableQueryable<ValveTemplate, object>> Includes = x => x
                    .Include(x => x.Valves);
                    Expression<Func<ValveTemplate, bool>> Criteria = x =>x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria:Criteria,Includes:Includes);

                    if (row == null) { return Result.Fail(Data.NotFound); }
                    foreach (var item in row.Valves)
                    {
                        item.ValveTemplateId = null;
                        await Repository.UpdateAsync(item);
                    }
                    await Repository.RemoveAsync(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.ValveTemplates.Cache.Key(row.Id));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
