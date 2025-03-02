using Server.Database.Entities.ProjectManagements;
using Server.EndPoint.Milestones.Queries;
using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.Deliverables.Records;
namespace Server.EndPoint.Deliverables.Queries
{
    public static class GetDeliverableByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.GetById, async (GetDeliverableByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Deliverable>, IIncludableQueryable<Deliverable, object>> Includes = x => null!;




                    Expression<Func<Deliverable, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Deliverables.Cache.GetById(request.Id);
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


        public static DeliverableResponse Map(this Deliverable row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
         

                ProjectId = row.ProjectId,
                Order = row.Order,

                WBS = row.WBS,
                StartDate = row.StartDate,
                EndDate = row.EndDate,
                ParentDeliverableId = row.ParentDeliverableId,
              
                DependencyType = string.IsNullOrEmpty(row.DependencyType) ? TasksRelationTypeEnum.None :
                TasksRelationTypeEnum.GetType(row.DependencyType),
                LabelOrder = row.LabelOrder,
                Duration=row.Duration,
                Lag=row.Lag

            };

        }



    }
}
