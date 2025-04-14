using Shared.Models.Templates.Valves.Requests;

namespace Server.EndPoint.Templates.Valves.Commands
{
    public static class DeleteGroupValveTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ValveTemplates.EndPoint.DeleteGroup, async (DeleteGroupValveTemplatesRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<ValveTemplate>, IIncludableQueryable<ValveTemplate, object>> Includes = x => x
                    .Include(x => x.Valves);
                    foreach (var rowItem in Data.SelecteItems)
                    {

                        Expression<Func<ValveTemplate, bool>> Criteria = x => x.Id == rowItem.Id;

                        var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                        if (row != null)
                        {
                            foreach (var item in row.Valves)
                            {
                                item.ValveTemplateId = null;
                                await Repository.UpdateAsync(item);
                            }
                            await Repository.RemoveAsync(row);
                        }
                    }


                    var cache = StaticClass.ValveTemplates.Cache.GetAll;

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache);
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
