
namespace Server.EndPoint.Cases.Queries
{
    public static class GetCaseByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Cases.EndPoint.GetById, async (GetCaseByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Case>, IIncludableQueryable<Case, object>> Includes = x => x
                    .Include(x => x.OrganizationStrategy!)
                    .Include(x => x.Scopes)
                    .Include(x => x.BackGrounds)
                    .Include(x => x.KnownRisks)
                    .Include(x => x.SucessfullFactors)
                    .Include(x => x.DecissionCriterias)
                    .Include(x => x.ExpertJudgements);

                    Expression<Func<Case, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Cases.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria, Includes: Includes);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map();
                    return Result.Success(response);

                });
            }
        }
        public static CaseResponse Map(this Case row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                ProjectId = row.ProjectId,

                IsNodeOpen = row.IsNodeOpen,
                Tab = row.Tab,

                BackGrounds = row.BackGrounds == null || row.BackGrounds.Count == 0 ? new() :
                row.BackGrounds.Select(x => x.Map(row.ProjectId)).ToList(),

                Scopes = row.Scopes == null || row.Scopes.Count == 0 ? new() :
                row.Scopes.Select(x => x.Map(row.ProjectId)).ToList(),

                CurrentScope = row.Scopes == null || row.Scopes.Count == 0 || row.Scopes.All(x => x.IsNodeOpen == false) ? null! :
                row.Scopes.First(x => x.IsNodeOpen == true).Map(row.ProjectId),

                KnownRisks = row.KnownRisks == null || row.KnownRisks.Count == 0 ? new() :
                row.KnownRisks.Select(x => x.Map(row.ProjectId)).ToList(),

                SucessfullFactors = row.SucessfullFactors == null || row.SucessfullFactors.Count == 0 ? new() :
                row.SucessfullFactors.Select(x => x.Map(row.ProjectId)).ToList(),

                DecissionCriterias = row.DecissionCriterias == null || row.DecissionCriterias.Count == 0 ? new() :
                row.DecissionCriterias.Select(x => x.Map(row.ProjectId)).ToList(),

                OrganizationStrategy = row.OrganizationStrategy == null ? null! : row.OrganizationStrategy.Map(),

                ExpertJudgements = row.ExpertJudgements == null || row.ExpertJudgements.Count == 0 ? new() :
                row.ExpertJudgements.Select(x => x.Map(row.ProjectId)).ToList(),
            };
        }
        public static CaseResponse MapShort(this Case row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                ProjectId = row.ProjectId,


            };
        }


    }
}
