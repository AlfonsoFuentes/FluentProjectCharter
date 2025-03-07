using Server.Database.Entities.ProjectManagements;
using Shared.Models.Acquisitions.Requests;

namespace Server.EndPoint.Acquisitions.Commands
{

    public static class CreateAcquisitionEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Acquisitions.EndPoint.Create, async (CreateAcquisitionRequest Data, IRepository Repository) =>
                {
                
                    var lastorder = await Repository.GetLastOrderAsync<Acquisition, Project>(Data.ProjectId);

                    var row = Acquisition.Create(Data.ProjectId,lastorder);

                    await Repository.AddAsync(row);



                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Acquisitions.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Acquisition Map(this CreateAcquisitionRequest request, Acquisition row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
