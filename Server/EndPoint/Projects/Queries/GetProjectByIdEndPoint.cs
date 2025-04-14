using Microsoft.EntityFrameworkCore.Query;
using Shared.Enums.CostCenter;
using Shared.Enums.Focuses;

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
                      .Include(x => x.BudgetItems)
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
            var result = new ProjectResponse()
            {
                Id = row.Id,

                Name = row.Name,
                Order = row.Order,

                PercentageEngineering = row.PercentageEngineering,
                PercentageContingency = row.PercentageContingency,
                ProjectNeedType = ProjectNeedTypeEnum.GetType(row.ProjectNeedType),

                ProjectNumber = row.ProjectNumber,

                Status = ProjectStatusEnum.GetType(row.Status),
                IsProductiveAsset= row.IsProductiveAsset,
                PercentageTaxProductive = row.PercentageTaxProductive,
                
                CostCenter = CostCenterEnum.GetTypeByName(row.CostCenter),
                Focus = FocusEnum.GetType(row.Focus),
                InitialProjectDate = row.StartDate,

                BudgetItems=row.BudgetItems.Count,

            };

            return result;
        }


    }
}
