using Shared.Models.Cases.Requests;
using System.Collections.Generic;

namespace Server.EndPoint.Cases.Commands
{

    public static class CreateCaseEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Cases.EndPoint.Create, async (CreateCaseRequest Data, IRepository Repository) =>
                {
                    var row = Case.Create(Data.ProjectId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.Cases.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Case Map(this CreateCaseRequest request, Case row)
        {
            row.Name = request.Name;
            row.OrganizationStrategyId = request.OrganizationStrategy == null ? null : request.OrganizationStrategy.Id;
            return row;
        }

    }

}
