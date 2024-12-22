using Server.EndPoint.Projects.Queries;
using Shared.Models.StakeHolders.Records;

namespace Server.EndPoint.StakeHolders.Queries
{
    public static class GetStakeHolderInsideProjectByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.StakeHolders.EndPoint.GetByIdInsideProject, async (GetStakeHolderInsideProjectByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<StakeHolder>, IIncludableQueryable<StakeHolder, object>> Includes = x => x.Include(x => x.RoleInsideProject!);

                    Expression<Func<StakeHolder, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.StakeHolders.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria,Includes:Includes);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.MapWithRole(request.ProjectId);
                    return Result.Success(response);

                });
            }

        }
        static StakeHolderInsideProjectResponse MapWithRole(this StakeHolder row, Guid ProjectId)
        {
            return new StakeHolderInsideProjectResponse()
            {
                StakeHolder = new()
                {
                    Id = row.Id,
                    Name = row.Name,
                    Area = row.Area,
                    Email = row.Email,
                    PhoneNumber = row.PhoneNumber,
                },
                ProjectId = ProjectId,
                Role = row.RoleInsideProject == null ? StakeHolderRoleEnum.None : StakeHolderRoleEnum.GetType(row.RoleInsideProject.Name)

            };

        }


    }
}
