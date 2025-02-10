using Shared.Models.Templates.Pipings.Requests;

namespace Server.EndPoint.Templates.Pipes.Commands
{
    public static class DeletePipeTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.PipeTemplates.EndPoint.Delete, async (DeletePipeTemplateRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<PipeTemplate>, IIncludableQueryable<PipeTemplate, object>> Includes = x => x
                    .Include(x => x.Isometrics);
                    Expression<Func<PipeTemplate, bool>> Criteria = x => x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);

                    if (row == null) { return Result.Fail(Data.NotFound); }
                    foreach (var item in row.Isometrics)
                    {
                        item.PipeTemplateId = null;
                        await Repository.UpdateAsync(item);
                    }
                    await Repository.RemoveAsync(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.PipeTemplates.Cache.Key(row.Id));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
