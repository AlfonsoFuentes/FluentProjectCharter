


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Backgrounds.Responses;
using Shared.Models.Bennefits.Responses;
using Shared.Models.Cases.Responses;
using Shared.Models.Constrainsts.Responses;
using Shared.Models.DecissionCriterias.Responses;
using Shared.Models.DeliverableRisks.Responses;
using Shared.Models.Deliverables.Responses;
using Shared.Models.KnownRisks.Responses;
using Shared.Models.OrganizationStrategies.Responses;
using Shared.Models.Requirements.Responses;
using Shared.Models.Scopes.Responses;
using Shared.Models.StakeHolders.Responses;
using Shared.Models.SucessfullFactors.Responses;

namespace Server.EndPoint.Projects.Queries
{
    public static class GetProjectByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.GetById, async (GetProjectByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x
                    .Include(x => x.Cases).ThenInclude(x => x.KnownRisks)
                    .Include(x => x.Cases).ThenInclude(x => x.BackGrounds)
                    .Include(x => x.Cases).ThenInclude(x => x.StakeHolders)
                    .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.Requirements)
                    .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.Assumptions)
                    .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.DeliverableRisks)
                    .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.Constraints)
                    .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.Bennefits)
                    .Include(x => x.Cases).ThenInclude(x => x.OrganizationStrategy)
                    .Include(x => x.Cases).ThenInclude(x => x.SucessfullFactors)
                    .Include(x => x.Cases).ThenInclude(x => x.DecissionCriterias)
                    ;

                    Expression<Func<Project, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Projects.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria, Includes: Includes);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map();
                    return Result.Success(response);

                });
            }
        }


        public static ProjectResponse Map(this Project row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                Cases = row.Cases.Count == 0 ? new() : row.Cases.Select(x => x.Map()).ToList(),

            };
        }
        public static CaseResponse Map(this Case row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                ProjectId = row.ProjectId,
                BackGrounds = row.BackGrounds.Count == 0 ? new() : row.BackGrounds.Select(x => x.Map()).ToList(),
                StakeHolders = row.StakeHolders.Count == 0 ? new() : row.StakeHolders.Select(x => x.Map()).ToList(),
                Scopes = row.Scopes.Count == 0 ? new() : row.Scopes.Select(x => x.Map()).ToList(),
                KnownRisks = row.KnownRisks.Count == 0 ? new() : row.KnownRisks.Select(x => x.Map()).ToList(),
                SucessfullFactors = row.SucessfullFactors.Count == 0 ? new() : row.SucessfullFactors.Select(x => x.Map()).ToList(),
                DecissionCriterias = row.DecissionCriterias.Count == 0 ? new() : row.DecissionCriterias.Select(x => x.Map()).ToList(),
                OrganizationStrategy = row.OrganizationStrategy == null ? null! : row.OrganizationStrategy.Map(),

            };
        }
        static BackGroundResponse Map(this BackGround row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                CaseId = row.CaseId,
                ProjectId = row.Case.ProjectId,
            };
        }
        static StakeHolderResponse Map(this StakeHolder row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                CaseId = row.CaseId,
                Email = row.Email,
                PhoneNumber = row.PhoneNumber,
                Role = row.Role,
                ProjectId = row.Case.ProjectId,
            };
        }
        static ScopeResponse Map(this Scope row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                CaseId = row.CaseId,
                ProjectId = row.Case.ProjectId,
                Deliverables = row.Deliverables.Count == 0 ? new() : row.Deliverables.Select(x => x.Map()).ToList(),
            };
        }
        static OrganizationStrategyResponse Map(this OrganizationStrategy row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,

            };
        }
        static KnownRiskResponse Map(this KnownRisk row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                CaseId = row.CaseId,
                ProjectId = row.Case.ProjectId,
            };
        }
        static SucessfullFactorResponse Map(this SucessfullFactor row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                CaseId = row.CaseId,
                ProjectId = row.Case.ProjectId
            };
        }
        static DecissionCriteriaResponse Map(this DecissionCriteria row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                CaseId = row.CaseId,
                ProjectId = row.Case.ProjectId,
            };
        }
        static DeliverableResponse Map(this Deliverable row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                ScopeId = row.ScopeId,
                Requirements = row.Requirements.Count == 0 ? new() : row.Requirements.Select(x => x.Map()).ToList(),
                Assumptions = row.Assumptions.Count == 0 ? new() : row.Assumptions.Select(x => x.Map()).ToList(),
                DeliverableRisks = row.DeliverableRisks.Count == 0 ? new() : row.DeliverableRisks.Select(x => x.Map()).ToList(),
                Constrainsts = row.Constraints.Count == 0 ? new() : row.Constraints.Select(x => x.Map()).ToList(),
                Bennefits = row.Bennefits.Count == 0 ? new() : row.Bennefits.Select(x => x.Map()).ToList(),
                ProjectId = row.Scope.Case.ProjectId,
            };
        }
        static RequirementResponse Map(this Requirement row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId,
                ProjectId = row.Deliverable.Scope.Case.ProjectId,

            };
        }
        static AssumptionResponse Map(this Assumption row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId,
                ProjectId = row.Deliverable.Scope.Case.ProjectId,
            };
        }
        static DeliverableRiskResponse Map(this DeliverableRisk row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId,
                ProjectId = row.Deliverable.Scope.Case.ProjectId,
            };
        }
        static ConstrainstResponse Map(this Constrainst row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId,
                ProjectId = row.Deliverable.Scope.Case.ProjectId,
            };
        }
        static BennefitResponse Map(this Bennefit row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId,
                ProjectId = row.Deliverable.Scope.Case.ProjectId,
            };
        }
    }
}
