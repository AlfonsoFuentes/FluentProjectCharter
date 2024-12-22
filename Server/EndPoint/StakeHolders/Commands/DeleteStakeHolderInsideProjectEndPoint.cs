﻿using Shared.Models.StakeHolders.Requests;

namespace Server.EndPoint.StakeHolders.Commands
{
    public static class DeleteStakeHolderInsideProjectEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.StakeHolders.EndPoint.DeleteInsideProject, async (DeleteStakeHolderInsideProjectRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> ProjectIncludes = x => x.Include(x => x.StakeHolders);
                    Expression<Func<Project, bool>> CriteriaProject = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Includes: ProjectIncludes, Criteria: CriteriaProject);

                    var stakeholder = await Repository.GetByIdAsync<StakeHolder>(Data.Id);

                    if (project == null || stakeholder == null) { return Result.Fail(Data.NotFound); }

                    project.StakeHolders.Remove(stakeholder);

                    await Repository.UpdateAsync(project);

                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.StakeHolders.Cache.Key(stakeholder.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}