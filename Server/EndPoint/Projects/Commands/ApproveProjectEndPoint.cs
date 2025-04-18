using Server.Database.Entities;

namespace Server.EndPoint.Projects.Commands
{
    public static class ApproveProjectEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.Approve, async (ApproveProjectRequest Data, IRepository Repository, IQueryRepository queryRepository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x
                      .Include(x => x.BudgetItems)
                     ;

                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);

                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);

                    var contingency = Contingency.Create(row.Id);
                    contingency.Order = 1;

                    var engineering = Engineering.Create(row.Id);
                    engineering.Order = 1;

                    var budgetUSD = row.BudgetItems.Sum(x => x.BudgetUSD);
                    var totalPercentage = Data.PercentageEngineering + Data.PercentageContingency;

                    engineering.Percentage = Data.PercentageEngineering;
                    contingency.Percentage = Data.PercentageContingency;

                    engineering.BudgetUSD = budgetUSD * Data.PercentageEngineering / (100 - totalPercentage);
                    contingency.BudgetUSD = budgetUSD * Data.PercentageContingency / (100 - totalPercentage);

                    engineering.Name = $"Engineering for {Data.Name} {Data.PercentageEngineering}%";
                    contingency.Name = $"Contingency for {Data.Name} {Data.PercentageContingency}%";

                    await Repository.AddAsync(contingency);
                    await Repository.AddAsync(engineering);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Projects.Cache.Key(row.Id));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }

        }

        static Project Map(this ApproveProjectRequest request, Project row)
        {
            row.Name = request.Name;

            row.StartDate = request.InitialProjectDate == null ? null : request.InitialProjectDate.Value;

            row.ProjectNumber = request.ProjectNumber;
            row.PercentageEngineering = request.PercentageEngineering;
            row.PercentageContingency = request.PercentageContingency;
            row.PercentageTaxProductive = request.PercentageTaxProductive;
            row.IsProductiveAsset = request.IsProductiveAsset;
            row.ProjectNeedType = request.ProjectNeedType.Id;
            row.CostCenter = request.CostCenter.Id;
            row.Focus = request.Focus.Id;
            row.Status = ProjectStatusEnum.Approved.Id;
            return row;
        }


    }

}
