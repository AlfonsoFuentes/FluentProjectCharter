using Shared.Models.AppStates.Requests;


namespace Server.EndPoint.AppStates.Commands
{
    public static partial class UpdateCaseTabStateEndPoint
    {
        public class EndPointProject : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.AppStates.EndPoint.UpdateCaseTab, async (UpdateActiveStateRequest Data, IRepository Repository) =>
                {
                    Expression<Func<Case, bool>> Criteria = x => x.Id ==
                   Data.CaseId;

                    var row = await Repository.GetAsync(Criteria: Criteria);
                    if (row == null)
                    {
                        return Result.Fail();
                    }
                    await Repository.UpdateAsync(row);
                    row.CaseTab = Data.CaseTab;

                    List<string> cache = [ .. StaticClass.AppStates.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());


                    return Result.EndPointResult(result,
                        "",
                        "");



                });
            }
        }

    }

}
