using Shared.Models.Backgrounds.Validators;
using Server.Database.Entities.ProjectManagements;
using Shared.Models.DeliverableGanttTasks.Validators;

namespace Server.EndPoint.NewDeliverablesGanttTasks.NewDeliverables.DeliverableGanttTasks
{
    public static class ValidateDeliverableGanttTasksNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.DeliverableGanttTasks.EndPoint.Validate, async (ValidateDeliverableRequest Data, IQueryRepository Repository) =>
                {
                    if(Data.IsDeliverable)
                    {
                        Expression<Func<Deliverable, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                        Func<Deliverable, bool> CriteriaExist = x => Data.Id == null ?
                        x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                        string CacheKey = StaticClass.DeliverableGanttTasks.Cache.GetAll(Data.ProjectId);

                        return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                    }
                    return false;
                });


            }
        }



    }

}
