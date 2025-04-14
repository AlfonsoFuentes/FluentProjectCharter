using Shared.Models.Templates.Pipings.Requests;

namespace Server.EndPoint.Templates.Pipes.Commands
{
    public static class DeleteGroupPipeTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.PipeTemplates.EndPoint.DeleteGroup, async (DeleteGroupPipeTemplatesRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<PipeTemplate>, IIncludableQueryable<PipeTemplate, object>> Includes = x => x
                    .Include(x => x.Isometrics);
                    foreach (var rowItem in Data.SelecteItems)
                    {

                        Expression<Func<PipeTemplate, bool>> Criteria = x => x.Id == rowItem.Id;

                        var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                        if (row != null)
                        {
                            foreach (var item in row.Isometrics)
                            {
                                item.PipeTemplateId = null;
                                await Repository.UpdateAsync(item);
                            }
                            await Repository.RemoveAsync(row);
                        }
                    }


                    var cache = StaticClass.PipeTemplates.Cache.GetAll;

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache);
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
