
using Server.EndPoint.Templates.Instruments.Queries;
using Server.ExtensionsMethods.InstrumentTemplateMapper;
using Shared.Models.Templates.Instruments.Responses;

namespace Server.EndPoint.Templates.Instruments.Commands
{

    public static class CreateInstrumentTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.InstrumentTemplates.EndPoint.Create, async (InstrumentTemplateResponse Data, IRepository Repository) =>
                {
                    var row = Template.AddInstrumentTemplate();

                    await Repository.AddAsync(row);

                    Data.Map(row);

                    await NozzleMapper.CreateNozzleTemplates(Repository, row.Id, Data.Nozzles);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.InstrumentTemplates.Cache.Key(row.Id));

                    return Result.EndPointResult(result,Data.Succesfully,
                        Data.Fail);


                });


            }
        }


       

    }

}
