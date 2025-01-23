


using Server.Database.Entities;
using Shared.Models.Requirements.Requests;
using System.Threading;

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
                    var row = Requirement.Create(Data.ProjectId, Data.ScopeId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Requirements.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Requirement Map(this CreateRequirementRequest request, Requirement row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
