using Server.Database.Entities.ProjectManagements;
using Shared.Models.Requirements.Requests;

namespace Server.EndPoint.Requirements.Commands
{

    public static class CreateRequirementEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Requirements.EndPoint.Create, async (CreateRequirementRequest Data, IRepository Repository) =>
                {
                
                    var lastorder = await Repository.GetLastOrderAsync<Requirement, Project>(Data.ProjectId);

                    var row = Requirement.Create(Data.ProjectId,lastorder);

                    await Repository.AddAsync(row);

                    row.Name = Data.Name;



                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
            private string[] GetCacheKeys(Requirement row)
            {
                List<string> cacheKeys = [           
                    .. StaticClass.Requirements.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static Requirement Map(this CreateRequirementRequest request, Requirement row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
