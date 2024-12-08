using Shared.Models.DecissionCriterias.Requests;

namespace Server.EndPoint.DecissionCriterias.Commands
{

    public static class CreateDecissionCriteriaEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.DecissionCriterias.EndPoint.Create, async (CreateDecissionCriteriaRequest Data, IRepository Repository) =>
                {
                    var row = DecissionCriteria.Create(Data.CaseId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.DecissionCriterias.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static DecissionCriteria Map(this CreateDecissionCriteriaRequest request, DecissionCriteria row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
