using Server.ExtensionsMethods.InstrumentTemplateMapper;
using Shared.Models.Templates.Instruments.Responses;

namespace Server.EndPoint.Templates.Instruments.Commands
{
    public static class UpdateInstrumentTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.InstrumentTemplates.EndPoint.Update, async (InstrumentTemplateResponse Data, IRepository Repository) =>
                {
                    Expression<Func<InstrumentTemplate, bool>> Criteria = x => x.Id == Data.Id;
                    Func<IQueryable<InstrumentTemplate>, IIncludableQueryable<InstrumentTemplate, object>> Includes = x => x
                    .Include(x => x.BrandTemplate!)
                    .Include(x => x.NozzleTemplates);
                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);

                    if (row == null) { return Result.Fail(Data.NotFound); }

                    await Repository.UpdateAsync(row);

                    Data.Map(row);

                    await NozzleMapper.UpdateNozzlesTemplate(Repository, row.NozzleTemplates, Data.Nozzles, row.Id);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.InstrumentTemplates.Cache.Key(row.Id));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
     
        }



      
    }

}
