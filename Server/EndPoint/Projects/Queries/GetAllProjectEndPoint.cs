


using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.EndPoint.Projects.Queries
{
    public static class GetAllProjectEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.GetAll, async (ProjectGetAll request, IMediator Mediator) =>
                {
                    return await Mediator.Send(new Query());
                });
            }
        }
        record Query : IRequest<IResult<ProjectResponseList>>
        {
            public Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x
                     .Include(x => x.Cases).ThenInclude(x => x.KnownRisks)
                     .Include(x => x.Cases).ThenInclude(x => x.BackGrounds)
                     .Include(x => x.Cases).ThenInclude(x => x.StakeHolders)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.Requirements)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.Assumptions)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.DeliverableRisks)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.Constraints)
                     .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables).ThenInclude(x => x.Bennefits)
                     .Include(x => x.Cases).ThenInclude(x => x.OrganizationStrategy)
                     .Include(x => x.Cases).ThenInclude(x => x.SucessfullFactors)
                     .Include(x => x.Cases).ThenInclude(x => x.DecissionCriterias);
            public string CacheKey => StaticClass.Projects.Cache.GetAll;
        }
        class QueryHandler : IRequestHandler<Query, IResult<ProjectResponseList>>
        {
            private IQueryRepository Repository;
            private IAppDbContext _cache;

            public QueryHandler(IAppDbContext cache, IQueryRepository repository)
            {
                _cache = cache;
                Repository = repository;
            }

            public async Task<IResult<ProjectResponseList>> Handle(Query request, CancellationToken cancellationToken)
            {
                var rows = await Repository.GetAllAsync(request.CacheKey, Includes: request.Includes);

                if (rows == null)
                {
                    return Result<ProjectResponseList>.Fail(
                    StaticClass.ResponseMessages.ReponseNotFound(StaticClass.Projects.ClassLegend));
                }

                var maps = rows.Select(x => x.Map()).ToList();


                ProjectResponseList response = new ProjectResponseList()
                {
                    Items = maps
                };
                return Result<ProjectResponseList>.Success(response);
            }
        }
        


    }
}
