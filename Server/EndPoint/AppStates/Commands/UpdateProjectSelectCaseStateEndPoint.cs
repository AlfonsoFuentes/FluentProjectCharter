using Shared.Models.AppStates.Requests;


namespace Server.EndPoint.AppStates.Commands
{
    public static partial class UpdateProjectSelectCaseStateEndPoint
    {
        public class EndPointProject : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.AppStates.EndPoint.UpdateProjectCase, async (UpdateActiveStateRequest Data, IRepository Repository) =>
                {
                    Expression<Func<Project, bool>> Criteria = x => x.Id ==
                   Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria);
                    if (project == null)
                    {
                        return Result.Fail();
                    }
                    await Repository.UpdateAsync(project);
                    project.CaseId = Data.CaseId;

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
