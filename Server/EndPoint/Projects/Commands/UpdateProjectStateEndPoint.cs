namespace Server.EndPoint.Projects.Commands
{
    public static class UpdateProjectStateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.UpdateState, async (ProjectResponse Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Project>(Data.Id);
                    if (row == null) { return Result.Fail(); }
                    await Repository.UpdateAsync(row);
                    
                    row.IsNodeOpen = Data.IsNodeOpen;
                    row.Tab = Data.Tab;
                    
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Projects.Cache.Key(row.Id));


                    return Result.Success();

                });
            }
        }



    }

}
