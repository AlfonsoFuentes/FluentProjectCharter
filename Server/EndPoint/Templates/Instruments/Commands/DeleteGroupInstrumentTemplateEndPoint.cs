using Shared.Models.Templates.Instruments.Requests;

namespace Server.EndPoint.Templates.Instruments.Commands
{
    public static class DeleteGroupInstrumentTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.InstrumentTemplates.EndPoint.DeleteGroup, async (DeleteGroupInstrumentTemplatesRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<InstrumentTemplate>, IIncludableQueryable<InstrumentTemplate, object>> Includes = x => x
                    .Include(x => x.Instruments);
                    foreach (var rowItem in Data.SelecteItems)
                    {

                        Expression<Func<InstrumentTemplate, bool>> Criteria = x => x.Id == rowItem.Id;

                        var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                        if (row != null)
                        {
                            foreach (var item in row.Instruments)
                            {
                                item.InstrumentTemplateId = null;
                                await Repository.UpdateAsync(item);
                            }
                            await Repository.RemoveAsync(row);
                        }
                    }


                    var cache = StaticClass.InstrumentTemplates.Cache.GetAll;

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache);
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
