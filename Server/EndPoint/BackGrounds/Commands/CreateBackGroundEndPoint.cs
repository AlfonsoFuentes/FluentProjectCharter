


using Server.Database.Entities;
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
                    var row = BackGround.Create(Data.CaseId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.BackGrounds.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                  

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static BackGround Map(this CreateBackGroundRequest request, BackGround row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
