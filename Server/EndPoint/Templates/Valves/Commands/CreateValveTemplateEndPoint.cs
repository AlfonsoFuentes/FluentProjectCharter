using Server.EndPoint.Templates.Valves.Commands;
using Server.EndPoint.Templates.Valves.Queries;
using Shared.Models.Templates.Valves.Requests;
using Shared.Models.Templates.Valves.Responses;

namespace Server.EndPoint.Templates.Valves.Commands
{

    public static class CreateValveTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ValveTemplates.EndPoint.Create, async (ValveTemplateResponse Data, IRepository Repository) =>
                {
                    var row = Template.AddValveTemplate();

                    await Repository.AddAsync(row);

                    Data.Map(row);

                    await NozzleMapper.CreateNozzleTemplates(Repository, row.Id, Data.Nozzles);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.ValveTemplates.Cache.Key(row.Id));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


       

    }

}
