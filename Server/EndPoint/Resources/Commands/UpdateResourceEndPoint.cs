using Shared.Models.Resources.Requests;


namespace Server.EndPoint.Resources.Commands
{
    public static class UpdateResourceEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Resources.EndPoint.Update, async (UpdateResourceRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Resource>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.Resources.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Resource Map(this UpdateResourceRequest request, Resource row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
