﻿using Shared.Models.Milestones.Requests;

namespace Server.EndPoint.Milestones.Commands
{
    public static class UpdateMilestoneDependencyTypeEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Milestones.EndPoint.UpdateDependencyType, async (UpdateMilestoneDependencyTypeRequest Data, IRepository Repository, IQueryRepository QueryRepository) =>
                {

                    var row = await Repository.GetByIdAsync<Milestone>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);

                  
                    row.DependencyType = Data.DependencyType;

                    Func<IQueryable<Milestone>, IIncludableQueryable<Milestone, object>> Includes = x => x
                  .Include(x => x.SubMilestones);

                    Expression<Func<Milestone, bool>> Criteria = x => x.ProjectId == Data.ProjectId;

                    var milestones = await Repository.GetAllAsync(Criteria: Criteria);

                    if (milestones == null) { return Result.Fail(Data.NotFound); }

                    List<string> cachekeys = new();
                    foreach (var milestone in milestones)
                    {
                        if (Data.Items.Any(x => x.Id == milestone.Id))
                        {
                            var item = Data.Items.First(x => x.Id == milestone.Id);
                            milestone.StartDate = item.StartDate;
                            milestone.EndDate = item.EndDate;
                            milestone.Duration = item.DurationInput;
                            await Repository.UpdateAsync(milestone);

                            cachekeys.AddRange(StaticClass.Milestones.Cache.Key(milestone.Id).ToList());
                        }
                    }


                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cachekeys.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            
        }




    }

}
