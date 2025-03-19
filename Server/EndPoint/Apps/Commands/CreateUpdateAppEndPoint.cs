using Server.EndPoint.Apps.Commands;
using Server.EndPoint.Projects.Commands;
using Shared.Models.Apps.Requests;

namespace Server.EndPoint.Apps.Commands
{
    public static class CreateUpdateAppEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Apps.EndPoint.Update, async (CreateUpdateAppRequest Data, IRepository Repository) =>
                {

                    var rows = await Repository.GetAllAsync<App>();
                    if (rows == null || rows.Count == 0)
                    {
                        var app = App.Create();
                        app.CurrentProjectId = Data.ProjectId;
                        await Repository.AddAsync(app);
                    }
                    else
                    {
                        var app = rows[0];
                        app.CurrentProjectId = Data.ProjectId;
                        await Repository.UpdateAsync(app);
                    }
                    var cache =new string []{ StaticClass.Projects.Cache.GetAll,StaticClass.Apps.Cache.GetAll};
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache);

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

            return row;
        }

    }

}
