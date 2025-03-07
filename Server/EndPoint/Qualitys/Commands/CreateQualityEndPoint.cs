using Server.Database.Entities.ProjectManagements;
using Shared.Models.Qualitys.Requests;

namespace Server.EndPoint.Qualitys.Commands
{

    public static class CreateQualityEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Qualitys.EndPoint.Create, async (CreateQualityRequest Data, IRepository Repository) =>
                {
                    
                    var lastorder = await Repository.GetLastOrderAsync<Quality, Project>(Data.ProjectId);

                    var row = Quality.Create(Data.ProjectId,lastorder);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Qualitys.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Quality Map(this CreateQualityRequest request, Quality row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
