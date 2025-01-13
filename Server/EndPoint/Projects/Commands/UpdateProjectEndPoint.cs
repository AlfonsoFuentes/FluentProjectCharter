namespace Server.EndPoint.Projects.Commands
{
    public static class UpdateProjectEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.Update, async (UpdateProjectRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Project>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Projects.Cache.Key(row.Id));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static Project Map(this UpdateProjectRequest request, Project row)
        {
            row.Name = request.Name;
            row.ProjectNeedType = request.ProjectNeedType.Name;
            row.InitialBudget = request.InitialBudget;
            row.ProjectDescription = request.ProjectDescription;
            row.ManagerId = request.Manager == null ? null : request.Manager.Id;
            row.SponsorId = request.Sponsor == null ? null : request.Sponsor.Id;
            row.Version0Date = request.InitialProjectDate == null ? null : request.InitialProjectDate.Value;
            row.Status = request.Status.Name;
            row.ProjectNumber = request.ProjectNumber;
            row.PercentageEngineering = request.PercentageEngineering;
            row.PercentageContingency = request.PercentageContingency;
            row.PercentageTaxProductive = request.PercentageTaxProductive;
            row.IsProductiveAsset = request.IsProductiveAsset;
            return row;
        }

    }

}
