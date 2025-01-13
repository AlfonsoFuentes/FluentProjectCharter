using Server.EndPoint.Deliverables.Queries;
using Shared.Models.Scopes.Records;

namespace Server.EndPoint.Scopes.Queries
{
    public static class GetScopeByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Scopes.EndPoint.GetById, async (GetScopeByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Scope>, IIncludableQueryable<Scope, object>> Includes = x => x
                    .Include(x => x.Deliverables).ThenInclude(x => x.AcceptanceCriterias)
                    .Include(x => x.Deliverables).ThenInclude(x => x.Requirements)
                    .Include(x => x.Deliverables).ThenInclude(x => x.Assumptions)
                    .Include(x => x.Deliverables).ThenInclude(x => x.DeliverableRisks)
                    .Include(x => x.Deliverables).ThenInclude(x => x.Constraints)
                    .Include(x => x.Deliverables).ThenInclude(x => x.Bennefits)

                    ;

                    Expression<Func<Scope, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Scopes.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria, Includes: Includes);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map(request.ProjectId);
                    return Result.Success(response);

                });
            }
        }
        public static ScopeResponse Map(this Scope row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                CaseId = row.CaseId,
                ProjectId = ProjectId,
                IsNodeOpen = row.IsNodeOpen,
                Tab = row.Tab,

                Deliverables = row.Deliverables == null || row.Deliverables.Count == 0 ? new() :
                row.Deliverables.Select(x => x.Map(ProjectId)).ToList(),

                CurrentDeliverable = row.Deliverables == null || row.Deliverables.Count == 0 ||
                row.Deliverables.All(x => x.IsNodeOpen == false) ? null! :
                row.Deliverables.First(x => x.IsNodeOpen == true).Map(ProjectId)
            };
        }


    }
}
