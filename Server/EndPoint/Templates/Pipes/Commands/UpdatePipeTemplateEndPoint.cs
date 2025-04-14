using Shared.Models.Templates.Pipings.Requests;
using Shared.Models.Templates.Pipings.Responses;
using Server.ExtensionsMethods.Pipings;
namespace Server.EndPoint.Templates.Pipes.Commands
{
    public static class UpdatePipeTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.PipeTemplates.EndPoint.Update, async (PipeTemplateResponse Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<PipeTemplate>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);

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
