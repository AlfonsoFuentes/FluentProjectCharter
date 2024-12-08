using Shared.Models.Scopes.Requests;

namespace Server.EndPoint.Scopes.Commands
{
    public static class UpdateScopeEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Scopes.EndPoint.Update, async (UpdateScopeRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Scope>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Scopes.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static Scope Map(this UpdateScopeRequest request, Scope row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
