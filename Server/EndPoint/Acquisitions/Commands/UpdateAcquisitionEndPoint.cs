using Shared.Models.Acquisitions.Requests;


namespace Server.EndPoint.Acquisitions.Commands
{
    public static class UpdateAcquisitionEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Acquisitions.EndPoint.Update, async (UpdateAcquisitionRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Acquisition>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.Acquisitions.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Acquisition Map(this UpdateAcquisitionRequest request, Acquisition row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
