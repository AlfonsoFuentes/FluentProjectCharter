

using Shared.Models.HighLevelRequirements.Responses;

namespace Server.EndPoint.Projects.Queries
{
    public static class GetAllProjectEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.GetAll, async (ProjectGetAll request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x
                     .Include(x => x.HighLevelRequirements)
                     .Include(x => x.StakeHolders).ThenInclude(x => x.RoleInsideProject!)
                     .Include(x => x.Manager)
                     .Include(x => x.Sponsor)
                     .Include(x => x.Cases).ThenInclude(x => x.KnownRisks)
                     .Include(x => x.Cases).ThenInclude(x => x.BackGrounds)

                     .Include(x => x.Cases).ThenInclude(x => x.ExpertJudgements).ThenInclude(x => x.Expert)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.Requirements)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.Assumptions)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.DeliverableRisks)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.Constraints)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.Bennefits)
                     .Include(x => x.Cases).ThenInclude(x => x.OrganizationStrategy)
                     .Include(x => x.Cases).ThenInclude(x => x.SucessfullFactors)
                     .Include(x => x.Cases).ThenInclude(x => x.DecissionCriterias);

                    string CacheKey = StaticClass.Projects.Cache.GetAll;
                    var rows = await Repository.GetAllAsync(CacheKey, Includes: Includes);
                    if (rows == null)
                    {
                        return Result<ProjectResponseList>.Fail(
                        StaticClass.ResponseMessages.ReponseNotFound(StaticClass.Projects.ClassLegend));
                    }
                    List<ProjectResponse> maps = rows.Count == 0 ? new() : rows.Select(x => x.Map()).ToList();


                    ProjectResponseList response = new ProjectResponseList()
                    {
                        Items = maps
                    };
                    return Result<ProjectResponseList>.Success(response);
                });
            }
        }

        public static ProjectResponse Map(this Project row)
        {
            return new()
            {
                Id = row.Id,

                Name = row.Name,

                ProjectNeedType = ProjectNeedTypeEnum.GetType(row.ProjectNeedType),

                Cases = row.Cases == null || row.Cases.Count == 0 ? new() : row.Cases.Select(x => x.Map()).ToList(),

                HighLevelRequirements = row.HighLevelRequirements == null || row.HighLevelRequirements.Count == 0 ? new() :
                row.HighLevelRequirements.Select(x => x.Map()).ToList(),

                StakeHolders = row.StakeHolders == null || row.StakeHolders.Count == 0 ? new() : row.StakeHolders.Select(x => x.MapInsideProject(row.Id)).ToList(),

                ProjectDescription = row.ProjectDescription,

                InitialBudget = row.InitialBudget,

                Manager = row.Manager == null ? null! : row.Manager.Map(),

                ProjectNumber = row.ProjectNumber,

                Sponsor = row.Sponsor == null ? null! : row.Sponsor.Map(),

            };
        }
        public static HighLevelRequirementResponse Map(this HighLevelRequirement row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                ProjectId = row.ProjectId,
            };
        }
        public static CaseResponse Map(this Case row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                ProjectId = row.ProjectId,
                BackGrounds = row.BackGrounds == null || row.BackGrounds.Count == 0 ? new() : row.BackGrounds.Select(x => x.Map(row.ProjectId)).ToList(),

                Scopes = row.Scopes == null || row.Scopes.Count == 0 ? new() : row.Scopes.Select(x => x.Map(row.ProjectId)).ToList(),
                KnownRisks = row.KnownRisks == null || row.KnownRisks.Count == 0 ? new() : row.KnownRisks.Select(x => x.Map(row.ProjectId)).ToList(),
                SucessfullFactors = row.SucessfullFactors == null || row.SucessfullFactors.Count == 0 ? new() :
                row.SucessfullFactors.Select(x => x.Map(row.ProjectId)).ToList(),
                DecissionCriterias = row.DecissionCriterias == null || row.DecissionCriterias.Count == 0 ? new() :
                row.DecissionCriterias.Select(x => x.Map(row.ProjectId)).ToList(),
                OrganizationStrategy = row.OrganizationStrategy == null ? null! : row.OrganizationStrategy.Map(),
                ExpertJudgements = row.ExpertJudgements == null || row.ExpertJudgements.Count == 0 ? new() :
                row.ExpertJudgements.Select(x => x.Map(row.ProjectId)).ToList(),
            };
        }
        public static BackGroundResponse Map(this BackGround row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                CaseId = row.CaseId,
                ProjectId = ProjectId,
            };
        }
        public static StakeHolderInsideProjectResponse MapInsideProject(this StakeHolder row, Guid _projectid)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                StakeHolder = new()
                {
                    Id = row.Id,
                    Name = row.Name,
                    PhoneNumber = row.PhoneNumber,
                    Email = row.Email,
                    Area = row.Area,

                },
                ProjectId = _projectid,
                Role = row.RoleInsideProject == null ? StakeHolderRoleEnum.None : StakeHolderRoleEnum.GetType(row.RoleInsideProject.Name),



            };
        }
        public static StakeHolderResponse Map(this StakeHolder row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                Area = row.Area,
                Email = row.Email,
                PhoneNumber = row.PhoneNumber,


            };
        }
        public static ExpertJudgementResponse Map(this ExpertJudgement row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                CaseId = row.CaseId,
                Expert = row.Expert == null ? null! : row.Expert.Map(),
                ProjectId = ProjectId,
            };
        }
        public static ScopeResponse Map(this Scope row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                CaseId = row.CaseId,
                ProjectId = ProjectId,
                Deliverables = row.Deliverables == null || row.Deliverables.Count == 0 ? new() : row.Deliverables.Select(x => x.Map(ProjectId)).ToList(),
            };
        }
        public static OrganizationStrategyResponse Map(this OrganizationStrategy row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,

            };
        }
        public static KnownRiskResponse Map(this KnownRisk row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                CaseId = row.CaseId,
                ProjectId = ProjectId,
            };
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
        public static DecissionCriteriaResponse Map(this DecissionCriteria row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                CaseId = row.CaseId,
                ProjectId = ProjectId,
            };
        }
        public static DeliverableResponse Map(this Deliverable row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                ScopeId = row.ScopeId,
                Requirements = row.Requirements == null || row.Requirements.Count == 0 ? new() : row.Requirements.Select(x => x.Map(ProjectId)).ToList(),

                Assumptions = row.Assumptions == null || row.Assumptions.Count == 0 ? new() : row.Assumptions.Select(x => x.Map(ProjectId)).ToList(),

                DeliverableRisks = row.DeliverableRisks == null || row.DeliverableRisks.Count == 0 ? new() :
                row.DeliverableRisks.Select(x => x.Map(ProjectId)).ToList(),

                Constrainsts = row.Constraints == null || row.Constraints.Count == 0 ? new() :
                row.Constraints.Select(x => x.Map(ProjectId)).ToList(),

                Bennefits = row.Bennefits == null || row.Bennefits.Count == 0 ? new() : row.Bennefits.Select(x => x.Map(ProjectId)).ToList(),

                ProjectId = ProjectId,
            };
        }
        public static RequirementResponse Map(this Requirement row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId,
                ProjectId = ProjectId,

            };
        }
        public static AssumptionResponse Map(this Assumption row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId,
                ProjectId = ProjectId,
            };
        }
        public static DeliverableRiskResponse Map(this DeliverableRisk row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId,
                ProjectId = ProjectId,
            };
        }
        public static ConstrainstResponse Map(this Constrainst row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId,
                ProjectId = ProjectId,
            };
        }
        public static BennefitResponse Map(this Bennefit row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId,
                ProjectId = ProjectId,
            };
        }

    }
}
