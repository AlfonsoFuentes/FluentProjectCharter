using Shared.Models.Constrainsts.Requests;


namespace Server.EndPoint.Constrainsts.Commands
{
    public static class UpdateConstrainstEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Constrainsts.EndPoint.Update, async (UpdateConstrainstRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Constrainst>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.Constrainsts.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Constrainst Map(this UpdateConstrainstRequest request, Constrainst row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
