using DocumentFormat.OpenXml.Spreadsheet;
using Shared.Models.AppStates.Requests;


namespace Server.EndPoint.AppStates.Commands
{
    public static partial class UpdateProjectStateEndPoint
    {
        public class EndPointProject : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.AppStates.EndPoint.UpdateActiveProject, async (UpdateActiveStateRequest Data, IRepository Repository) =>
                {
                    var projects = await Repository.GetAllAsync<AppState>();
                    if (projects == null)
                    {
                        return Result.Fail();
                    }
                    var project = projects.FirstOrDefault();
                    if (project == null)
                    {
                        project = AppState.Create();
                        await Repository.AddAsync(project);
                    }
                    else
                    {
                        await Repository.UpdateAsync(project);
                    }
                    project.ActiveProjectId = Data.ProjectId;

                    List<string> cache = [.. StaticClass.AppStates.Cache.Key(project.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());


                    return Result.EndPointResult(result,
                        "",
                        "");



                });
            }
        }

    }

}
