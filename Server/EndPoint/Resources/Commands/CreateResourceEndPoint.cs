using Server.Database.Entities.ProjectManagements;
using Shared.Models.Resources.Requests;

namespace Server.EndPoint.Resources.Commands
{

    public static class CreateResourceEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Resources.EndPoint.Create, async (CreateResourceRequest Data, IRepository Repository) =>
                {
                  
                    var lastorder = await Repository.GetLastOrderAsync<Resource, Project>(Data.ProjectId);

                    var row = Resource.Create(Data.ProjectId,lastorder);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Resources.Cache.Key(row.Id, row.ProjectId)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Resource Map(this CreateResourceRequest request, Resource row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
