using Server.EndPoint.Alterations.Queries;
using Server.EndPoint.EHSs.Queries;
using Server.EndPoint.Electricals.Queries;
using Server.EndPoint.EngineeringDesigns.Queries;
using Server.EndPoint.Equipments.Queries;
using Server.EndPoint.Foundations.Queries;
using Server.EndPoint.Instruments.Queries;
using Server.EndPoint.Paintings.Queries;
using Server.EndPoint.StakeHolders.Queries;
using Server.EndPoint.Structurals.Queries;
using Server.EndPoint.Taxs.Queries;
using Server.EndPoint.Testings.Queries;
using Server.EndPoint.Valves.Queries;
using Server.EndPoint.Pipes.Queries;
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
                     .Include(x => x.Requirements.Where(x => x.Scope == null))
                     .Include(x => x.Assumptions.Where(x => x.Scope == null))
                     .Include(x => x.Constrainsts.Where(x => x.Scope == null))
                     .Include(x => x.StakeHolders).ThenInclude(x => x.RoleInsideProject!)
                     .Include(x => x.Manager)
                     .Include(x => x.Sponsor)
                     .Include(x => x.Meetings).ThenInclude(x => x.MeetingAttendants).ThenInclude(x => x.StakeHolder!)
                     .Include(x => x.Meetings).ThenInclude(x => x.MeetingAgreements)
                     .Include(x => x.BudgetItems)

                     .Include(x => x.Cases).ThenInclude(x => x.OrganizationStrategy!)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.AcceptanceCriterias)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.DeliverableRisks)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Requirements)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Assumptions)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Constraints)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Bennefits)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.BudgetItems)


                    .Include(x => x.Cases).ThenInclude(x => x.BackGrounds)
                    .Include(x => x.Cases).ThenInclude(x => x.KnownRisks)
                    .Include(x => x.Cases).ThenInclude(x => x.SucessfullFactors)
                    .Include(x => x.Cases).ThenInclude(x => x.DecissionCriterias)
                    .Include(x => x.Cases).ThenInclude(x => x.ExpertJudgements).ThenInclude(x => x.Expert!);

                    ;


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
                        CurrentProject = maps.FirstOrDefault(x => x.IsNodeOpen),
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

                IsNodeOpen = row.IsNodeOpen,
                Tab = row.Tab,
                PercentageEngineering = row.PercentageEngineering,
                PercentageContingency = row.PercentageContingency,
                ProjectNeedType = ProjectNeedTypeEnum.GetType(row.ProjectNeedType),
                Requirements = row.Requirements == null || row.Requirements.Count == 0 ? new() : row.Requirements.Select(x => x.Map()).ToList(),
                Assumptions = row.Assumptions == null || row.Assumptions.Count == 0 ? new() : row.Assumptions.Select(x => x.Map()).ToList(),
                Constrainsts = row.Constrainsts == null || row.Constrainsts.Count == 0 ? new() : row.Constrainsts.Select(x => x.Map()).ToList(),

                Cases = row.Cases == null || row.Cases.Count == 0 ? new() : row.Cases.Select(x => x.Map()).ToList(),
                CurrentCase = row.Cases == null || row.Cases.Count == 0 || row.Cases.All(x => x.IsNodeOpen == false) ? null! : row.Cases.First(x => x.IsNodeOpen).Map(),

                HighLevelRequirements = row.HighLevelRequirements == null || row.HighLevelRequirements.Count == 0 ? new() :
                row.HighLevelRequirements.Select(x => x.Map()).ToList(),

                StakeHolders = row.StakeHolders == null || row.StakeHolders.Count == 0 ? new() :
                row.StakeHolders.Select(x => x.MapInsideProject(row.Id)).ToList(),

                ProjectDescription = row.ProjectDescription,

                InitialBudget = row.InitialBudget,

                Manager = row.Manager == null ? null! : row.Manager.Map(),

                ProjectNumber = row.ProjectNumber,

                Sponsor = row.Sponsor == null ? null! : row.Sponsor.Map(),
                Status = ProjectStatusEnum.GetType(row.Status),
                IsProductive = row.IsProductiveAsset,
                PercentageTaxes = row.PercentageTaxProductive,

                Meetings = row.Meetings == null || row.Meetings.Count == 0 ? new() : row.Meetings.Select(x => x.Map()).ToList(),
                CurrentMeeting = row.Meetings == null || row.Meetings.Count == 0 || row.Meetings.All(x => x.IsNodeOpen == false) ? null! : row.Meetings.First(x => x.IsNodeOpen).Map(),

                Alterations = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Alteration>().Select(x => x.Map()).ToList(),
                Structurals = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Structural>().Select(x => x.Map()).ToList(),
                Foundations = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Foundation>().Select(x => x.Map()).ToList(),
                Equipments = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Equipment>().Select(x => x.Map()).ToList(),

                Valves = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Valve>().Select(x => x.Map()).ToList(),
                Electricals = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Electrical>().Select(x => x.Map()).ToList(),
                Pipings = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Pipe>().Select(x => x.Map()).ToList(),
                Instruments = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Instrument>().Select(x => x.Map()).ToList(),

                EHSs = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<EHS>().Select(x => x.Map()).ToList(),
                Paintings = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Painting>().Select(x => x.Map()).ToList(),
                Taxes = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Tax>().Select(x => x.Map()).ToList(),
                Testings = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Testing>().Select(x => x.Map()).ToList(),

                EngineeringDesigns = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<EngineeringDesign>().Select(x => x.Map()).ToList(),

            };
        }


    }
}
