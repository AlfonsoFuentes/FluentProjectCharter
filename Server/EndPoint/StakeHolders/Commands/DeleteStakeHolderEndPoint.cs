using Shared.Models.StakeHolders.Requests;

namespace Server.EndPoint.StakeHolders.Commands
{
    public static class DeleteStakeHolderEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.StakeHolders.EndPoint.Delete, async (DeleteStakeHolderRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<StakeHolder>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }

                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x
                     .Include(x => x.StakeHolders).ThenInclude(x => x.RoleInsideProject!);
                    Expression<Func<Project, bool>> CriteriaProject = x => x.StakeHolders.Any(x => x.Id == Data.Id);


                    var projects = await Repository.GetAllAsync(Includes: Includes, Criteria: CriteriaProject);

                    List<string> cache = [.. StaticClass.StakeHolders.Cache.Key(row.Id)];

                    projects.ForEach(async x =>
                    {
                        x.StakeHolders.RemoveAll(x => x.Id == Data.Id);
                        cache.AddRange(StaticClass.Projects.Cache.Key(x.Id).ToList());
                        await Repository.UpdateAsync(x);
                    });


                    await Repository.RemoveAsync(row);



                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
