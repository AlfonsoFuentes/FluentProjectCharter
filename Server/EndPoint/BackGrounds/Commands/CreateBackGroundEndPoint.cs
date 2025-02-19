using Server.Database.Entities.ProjectManagements;
using Shared.Models.Backgrounds.Requests;
using System.Threading;

namespace Server.EndPoint.BackGrounds.Commands
{

    public static class CreateBackGroundEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.BackGrounds.EndPoint.Create, async (CreateBackGroundRequest Data, IRepository Repository) =>
                {
                    var lastorder = await Repository.GetLastOrderAsync<BackGround, Project>(Data.ProjectId);


                    BackGround row = BackGround.Create(Data.ProjectId, lastorder);


                    await Repository.AddAsync(row);

                    row.Name = Data.Name;
                    List<string> cache = [.. StaticClass.BackGrounds.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


    }

}
