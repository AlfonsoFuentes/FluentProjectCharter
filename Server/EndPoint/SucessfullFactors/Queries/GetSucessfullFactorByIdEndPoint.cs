using Server.EndPoint.Projects.Queries;
using Shared.Models.SucessfullFactors.Records;

namespace Server.EndPoint.SucessfullFactors.Queries
{
    public static class GetSucessfullFactorByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.SucessfullFactors.EndPoint.GetById, async (GetSucessfullFactorByIdRequest request, IQueryRepository Repository) =>
                {
                    //Func<IQueryable<SucessfullFactor>, IIncludableQueryable<SucessfullFactor, object>> Includes = x => null!
               
                    //;

                    Expression<Func<SucessfullFactor, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.SucessfullFactors.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map(request.ProjectId);
                    return Result.Success(response);

                });
            }
        }
        public static SucessfullFactorResponse Map(this SucessfullFactor row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                CaseId = row.CaseId,
                ProjectId = ProjectId
            };
        }


    }
}
