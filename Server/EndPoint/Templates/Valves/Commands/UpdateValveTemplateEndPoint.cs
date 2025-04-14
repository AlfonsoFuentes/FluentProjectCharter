using Shared.Models.Templates.Valves.Responses;

namespace Server.EndPoint.Templates.Valves.Commands
{
    public static class UpdateValveTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ValveTemplates.EndPoint.Update, async (ValveTemplateResponse Data, IRepository Repository) =>
                {
                    Func<IQueryable<ValveTemplate>, IIncludableQueryable<ValveTemplate, object>> Includes = x => x
                    .Include(x => x.NozzleTemplates)
                    .Include(x => x.BrandTemplate!);
                    ;

                    Expression<Func<ValveTemplate, bool>> Criteria = x => x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);

                    Data.Map(row);

                    await NozzleMapper.UpdateNozzlesTemplate(Repository, row.NozzleTemplates, Data.Nozzles, row.Id);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.ValveTemplates.Cache.Key(row.Id));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
           


        }



    }

}
