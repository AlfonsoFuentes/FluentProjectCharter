namespace Server.EndPoint.Requirements.Queries
{
    public static class GetRequirementByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Requirements.EndPoint.GetById, async (GetRequirementByIdRequest request, IQueryRepository Repository) =>
                {
                    //Func<IQueryable<Requirement>, IIncludableQueryable<Requirement, object>> Includes = x => null!
               
                    //;

                    Expression<Func<Requirement, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Requirements.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map(request.ProjectId);
                    return Result.Success(response);

                });
            }
        }

        public static RequirementResponse Map(this Requirement row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId,
                //SubDeliverableId = row.SubDeliverableId,
                ProjectId = ProjectId,

            };
        }

    }
}
