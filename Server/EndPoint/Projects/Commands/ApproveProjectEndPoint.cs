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
                    var row = await Repository.GetByIdAsync<Project>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    var contingency = Contingency.Create(row.Id);
                    var engineering = Engineering.Create(row.Id);
                    engineering.Percentage = Data.PercentageEngineering;
                    contingency.Percentage = Data.PercentageContingency;
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
            row.ProjectNeedType = request.ProjectNeedType.Name;

          
            row.StartDate = request.InitialProjectDate == null ? null : request.InitialProjectDate.Value;
            row.Status = ProjectStatusEnum.Approved.Name;
            row.ProjectNumber = request.ProjectNumber;
            row.PercentageEngineering = request.PercentageEngineering;
            row.PercentageContingency = request.PercentageContingency;
            row.PercentageTaxProductive = request.PercentageTaxProductive;
            row.IsProductiveAsset = request.IsProductiveAsset;
            row.CostCenter=request.CostCenter.Name;
            row.Focus=request.Focus.Name;
            return row;
        }


    }

}
