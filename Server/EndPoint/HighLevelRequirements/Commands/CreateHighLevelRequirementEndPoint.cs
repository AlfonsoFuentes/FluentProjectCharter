using Shared.Models.HighLevelRequirements.Requests;
using System.Collections.Generic;

namespace Server.EndPoint.HighLevelRequirements.Commands
{

    public static class CreateHighLevelRequirementEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.HighLevelRequirements.EndPoint.Create, async (CreateHighLevelRequirementRequest Data, IRepository Repository) =>
                {
                    var row = HighLevelRequirement.Create(Data.ProjectId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.HighLevelRequirements.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static HighLevelRequirement Map(this CreateHighLevelRequirementRequest request, HighLevelRequirement row)
        {
            row.Name = request.Name;
         
            return row;
        }

    }

}
