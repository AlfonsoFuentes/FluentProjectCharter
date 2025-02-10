using Server.EndPoint.AcceptanceCriterias.Queries;
using Server.EndPoint.Constrainsts.Queries;
using Server.EndPoint.Deliverables.Queries;
using Server.EndPoint.Milestones.Queries;
using Server.EndPoint.Objectives.Queries;
using Server.EndPoint.StakeHolders.Queries;

namespace Server.EndPoint.Projects.Queries
{
    public static class GetCompleteProjectByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.GetCompleteById, async (GetCompleteProjectByIdRequest request, IQueryRepository Repository) =>
                {
                   
                    Expression<Func<Project, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Projects.Cache.GetCompleteById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria);

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
            var result = new ProjectResponse()
            {
                Id = row.Id,

                Name = row.Name,
                Order = row.Order,

                PercentageEngineering = row.PercentageEngineering,
                PercentageContingency = row.PercentageContingency,
                ProjectNeedType = ProjectNeedTypeEnum.GetType(row.ProjectNeedType),
                Manager = row.Manager == null ? null! : row.Manager.Map(),
                ProjectNumber = row.ProjectNumber,
                Sponsor = row.Sponsor == null ? null! : row.Sponsor.Map(),
                Status = ProjectStatusEnum.GetType(row.Status),
                IsProductive = row.IsProductiveAsset,
                PercentageTaxes = row.PercentageTaxProductive,
                PlanningId = row.PlanningId,
                StartId = row.StartId,
                ClosingId = row.ClosingId,
                ExecutingId = row.ExecutingId,
                InitialProjectDate = row.StartDate,
                MonitoringId = row.MonitoringId,


            };

            return result;
        }


    }
}
