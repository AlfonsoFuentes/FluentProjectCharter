using Shared.Models.AcceptanceCriterias.Records;
using Shared.Models.AcceptanceCriterias.Responses;

namespace Server.EndPoint.AcceptanceCriterias.Queries
{
    public static class GetAcceptanceCriteriaByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.AcceptanceCriterias.EndPoint.GetById, async (GetAcceptanceCriteriaByIdRequest request, IQueryRepository Repository) =>
                {
                    //Func<IQueryable<AcceptanceCriteria>, IIncludableQueryable<AcceptanceCriteria, object>> Includes = x => null!
               
                    //;

                    Expression<Func<AcceptanceCriteria, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.AcceptanceCriterias.Cache.GetById(request.Id);
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

        public static AcceptanceCriteriaResponse Map(this AcceptanceCriteria row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId,
                //SubDeliverableId = row.SubDeliverableId,
                ProjectId = ProjectId,

            };
        }

    }
}
