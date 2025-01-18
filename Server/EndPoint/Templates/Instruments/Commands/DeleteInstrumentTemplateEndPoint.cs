﻿



using Server.Database.Entities;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.Templates.Instruments.Requests;

namespace Server.EndPoint.InstrumentTemplates.Commands
{
    public static class DeleteInstrumentTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.InstrumentTemplates.EndPoint.Delete, async (DeleteInstrumentTemplateRequest Data, IRepository Repository) =>
                {
                    try
                    {
                        if (Data == null || Data.Id == Guid.Empty)
                        {
                            return Result.Fail("Invalid request data.");
                        }

                        Func<IQueryable<InstrumentTemplate>, IIncludableQueryable<InstrumentTemplate, object>> Includes = x => x
                            .Include(x => x.Instruments);
                        Expression<Func<InstrumentTemplate, bool>> Criteria = x => x.Id == Data.Id;

                        var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);

                        if (row == null)
                        {
                            return Result.Fail(Data.NotFound);
                        }

                        foreach (var item in row.Instruments)
                        {
                            item.InstrumentTemplateId = null;
                            await Repository.UpdateAsync(item);
                        }

                        await Repository.RemoveAsync(row);

                        var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.InstrumentTemplates.Cache.Key(row.Id));

                        return Result.EndPointResult(result, Data.Succesfully, Data.Fail);
                    }
                    catch (Exception ex)
                    {
                        string esm = ex.Message;
                        // Log the exception (logging mechanism not shown here)
                        return Result.Fail("An error occurred while processing the request.");
                    }
                });
            }
        }
    }
}
