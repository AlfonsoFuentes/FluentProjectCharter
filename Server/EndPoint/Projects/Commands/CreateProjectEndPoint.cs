namespace Server.EndPoint.Projects.Commands
{

    public static class CreateProjectEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.Create, async (CreateProjectRequest Data, IRepository Repository, IQueryRepository queryRepository) =>
                {
                    var projects = await queryRepository.GetAllAsync<Project>(Cache: StaticClass.Projects.Cache.GetAll);

                    int lastorder = projects == null || projects.Count == 0 ? 1 : projects.MaxBy(x => x.Order)!.Order + 1;



                    var row = Project.Create(lastorder);
                    row.Name = Data.Name;
                   
                    await Repository.AddAsync(row);


                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Projects.Cache.Key(row.Id));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }

        }


        static Project Map(this CreateProjectRequest request, Project row)
        {
            row.Name = request.Name;
            row.ProjectNeedType = request.ProjectNeedType.Name;

            row.StartDate = request.InitialProjectDate;
            row.Status = request.Status.Name;
            row.PercentageEngineering = request.PercentageEngineering;
            row.PercentageContingency = request.PercentageContingency;
            row.PercentageTaxProductive = request.PercentageTaxProductive;
            row.IsProductiveAsset = request.IsProductiveAsset;
            row.CostCenter = request.CostCenter.Name;
            row.Focus = request.Focus.Name;
            return row;
        }

    }

}
