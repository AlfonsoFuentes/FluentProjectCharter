using Server.Database.Entities.ProjectManagements;
using Shared.Enums.TasksRelationTypes;
using Shared.Models.GanttTasks.Records;
using Shared.Models.GanttTasks.Responses;

namespace Server.EndPoint.GanttTasks.Queries
{
    //public static class GetGanttTaskByIdEndPoint
    //{
    //    public class EndPoint : IEndPoint
    //    {
    //        public void MapEndPoint(IEndpointRouteBuilder app)
    //        {
    //            app.MapPost(StaticClass.GanttTasks.EndPoint.GetById, async (GetGanttTaskByIdRequest request, IQueryRepository Repository) =>
    //            {
    //                Func<IQueryable<GanttTask>, IIncludableQueryable<GanttTask, object>> Includes = x => null!;




    //                Expression<Func<GanttTask, bool>> Criteria = x => x.Id == request.Id;

    //                string CacheKey = StaticClass.GanttTasks.Cache.GetById(request.Id);
    //                var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria, Includes: Includes);

    //                if (row == null)
    //                {
    //                    return Result.Fail(request.NotFound);
    //                }

    //                var response = row.Map();
    //                return Result.Success(response);

    //            });
    //        }
    //    }


    //    public static GanttTaskResponse Map(this GanttTask row)
    //    {
    //        return new()
    //        {
    //            Id = row.Id,
    //            Name = row.Name,
    //            ProjectId = row.Deliverable == null ? Guid.Empty : row.Deliverable.ProjectId,
    //            DeliverableId = row.DeliverableId,
    //            Order = row.Order,
    //            WBS = row.WBS,
    //            StartDate = row.StartDate,
    //            EndDate = row.EndDate,
    //            ParentGanttTaskId = row.ParentId,
    //            DependencyType = string.IsNullOrEmpty(row.DependencyType) ? TasksRelationTypeEnum.None :
    //            TasksRelationTypeEnum.GetType(row.DependencyType),
    //            LabelOrder = row.LabelOrder,
    //            Duration = row.Duration,
    //            Lag = row.Lag

    //        };

    //    }



    //}
}
