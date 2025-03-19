﻿using Server.Database.Entities.ProjectManagements;
using Shared.Models.StakeHolderInsideProjects.Requests;

namespace Server.EndPoint.StakeHolderInsideProjects.Commands
{
    public static class UpdateStakeHolderInsideProjectEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.StakeHolderInsideProjects.EndPoint.Update, async (UpdateStakeHolderInsideProjectRequest Data, IRepository Repository) =>
                {
                    Expression<Func<StakeHolder, bool>> CriteriaStakeHolder = x => x.Id == Data.StakeHolder.Id;
                    Func<IQueryable<StakeHolder>, IIncludableQueryable<StakeHolder, object>> StakeHolderIncludes = x =>
                   x.Include(x => x.RoleInsideProject!);

                    var row = await Repository.GetAsync(Criteria: CriteriaStakeHolder, Includes: StakeHolderIncludes);
                    if (row == null || row.RoleInsideProjectId == null) return Result.Fail(Data.Fail);

                    var rolinsideproject = await Repository.GetByIdAsync<RoleInsideProject>(row.RoleInsideProjectId.Value);
                    if (rolinsideproject == null) return Result.Fail(Data.Fail);
                    if (Data.Role.Id == StakeHolderRoleEnum.None.Id)
                    {
                        return Result.Fail($"Stake Holder Role must be defined!!");
                    }
                    rolinsideproject.Name = Data.Role.Name;

                    await Repository.UpdateAsync(rolinsideproject);

                    List<string> cache = [.. StaticClass.StakeHolders.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


    }

}
