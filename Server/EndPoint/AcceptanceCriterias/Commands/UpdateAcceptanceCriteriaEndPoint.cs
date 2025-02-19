using Server.Database.Entities.ProjectManagements;
using Shared.Models.AcceptanceCriterias.Requests;


namespace Server.EndPoint.AcceptanceCriterias.Commands
{
    public static class UpdateAcceptanceCriteriaEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.AcceptanceCriterias.EndPoint.Update, async (UpdateAcceptanceCriteriaRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<AcceptanceCriteria>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [.. StaticClass.AcceptanceCriterias.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static AcceptanceCriteria Map(this UpdateAcceptanceCriteriaRequest request, AcceptanceCriteria row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
