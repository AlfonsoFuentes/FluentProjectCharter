using Server.EndPoint.Templates.Pipes.Commands;
using Server.EndPoint.Templates.Pipes.Queries;
using Server.ExtensionsMethods.Pipings;
using Shared.Models.Templates.Pipings.Requests;
using Shared.Models.Templates.Pipings.Responses;

namespace Server.EndPoint.Templates.Pipes.Commands
{

    public static class CreatePipeTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.PipeTemplates.EndPoint.Create, async (PipeTemplateResponse Data, IRepository Repository) =>
                {
                    var row = Template.AddPipeTemplate();

                    await Repository.AddAsync(row);

                    Data.Map(row);

                 

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.PipeTemplates.Cache.Key(row.Id));

                    return Result.EndPointResult(result, 
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


       

    }

}
