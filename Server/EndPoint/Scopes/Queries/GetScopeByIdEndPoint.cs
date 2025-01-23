using Server.EndPoint.Deliverables.Queries;
using Shared.Models.Scopes.Records;
using Server.EndPoint.AcceptanceCriterias.Queries;

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
                    .Include(x => x.AcceptanceCriterias)
                    .Include(x => x.DeliverableRisks)
                    .Include(x => x.Constraints)
                    .Include(x => x.Requirements)
                    .Include(x => x.Assumptions)
                    .Include(x => x.Bennefits)
                    .Include(x => x.Deliverables).ThenInclude(x => x.BudgetItems)
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
                row.Deliverables.OrderBy(x => x.Order).Select(x => x.Map(ProjectId)).ToList(),

                CurrentDeliverable = row.Deliverables == null || row.Deliverables.Count == 0 ||
                row.Deliverables.OrderBy(x => x.Order).All(x => x.IsNodeOpen == false) ? null! :
                row.Deliverables.OrderBy(x => x.Order).First(x => x.IsNodeOpen == true).Map(ProjectId),

                Requirements = row.Requirements == null || row.Requirements.Count == 0 ? new() : row.Requirements.Select(x => x.Map()).ToList(),

                Assumptions = row.Assumptions == null || row.Assumptions.Count == 0 ? new() : row.Assumptions.Select(x => x.Map()).ToList(),

                DeliverableRisks = row.DeliverableRisks == null || row.DeliverableRisks.Count == 0 ? new() :
                row.DeliverableRisks.Select(x => x.Map(ProjectId)).ToList(),

                Constrainsts = row.Constraints == null || row.Constraints.Count == 0 ? new() :
                row.Constraints.Select(x => x.Map()).ToList(),

                Bennefits = row.Bennefits == null || row.Bennefits.Count == 0 ? new() : row.Bennefits.Select(x => x.Map(ProjectId)).ToList(),

                AcceptanceCriterias = row.AcceptanceCriterias == null || row.AcceptanceCriterias.Count == 0 ? new() :
                row.AcceptanceCriterias.Select(x => x.Map(ProjectId)).ToList(),
            };
        }


    }
}
