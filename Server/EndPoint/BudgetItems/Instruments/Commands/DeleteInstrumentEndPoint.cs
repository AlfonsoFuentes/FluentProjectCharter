using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Instruments.Requests;

namespace Server.EndPoint.Instruments.Commands
{
    public static class DeleteInstrumentEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Instruments.EndPoint.Delete, async (DeleteInstrumentRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Instrument>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Instruments.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
