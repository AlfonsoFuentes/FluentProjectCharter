namespace Server.EndPoint.Projects.Commands
{

    public static class CreateUpdateProjectEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.CreateUpdate, async (ProjectResponse Data, IRepository Repository, IQueryRepository queryRepository) =>
                {
                    Project? row;
                    if (Data.Id == Guid.Empty)
                    {
                        var projects = await queryRepository.GetAllAsync<Project>(Cache: StaticClass.Projects.Cache.GetAll);

                        int lastorder = projects == null || projects.Count == 0 ? 1 : projects.MaxBy(x => x.Order)!.Order + 1;
                        row = Project.Create(lastorder);
                        await Repository.AddAsync(row);
                    }
                    else
                    {
                        row = await Repository.GetByIdAsync<Project>(Data.Id);
                        if (row == null) { return Result.Fail(Data.NotFound); }
                        await Repository.UpdateAsync(row);
                    }


                    Data.Map(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Projects.Cache.Key(row.Id));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }

        }


        static Project Map(this ProjectResponse request, Project row)
        {
            row.Name = request.Name;
            row.ProjectNeedType = request.ProjectNeedType.Name;

            row.StartDate = request.InitialProjectDate == null ? null : request.InitialProjectDate.Value;


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
