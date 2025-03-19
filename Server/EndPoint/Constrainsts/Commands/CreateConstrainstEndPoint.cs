using Server.Database.Entities.ProjectManagements;
using Shared.Models.Constrainsts.Requests;

namespace Server.EndPoint.Constrainsts.Commands
{

    public static class CreateConstrainstEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Constrainsts.EndPoint.Create, async (CreateConstrainstRequest Data, IRepository Repository) =>
                {
              
                    var lastorder = await Repository.GetLastOrderAsync<Constrainst, Project>(Data.ProjectId);

                    var row = Constrainst.Create(Data.ProjectId,lastorder);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                   

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
            private string[] GetCacheKeys(Constrainst row)
            {
                List<string> cacheKeys = [                
                    .. StaticClass.Constrainsts.Cache.Key(row.Id, row.ProjectId)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static Constrainst Map(this CreateConstrainstRequest request, Constrainst row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
