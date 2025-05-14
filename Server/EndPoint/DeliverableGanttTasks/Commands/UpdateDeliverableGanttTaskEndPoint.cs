using Shared.Models.DeliverableGanttTasks.Responses;

namespace Server.EndPoint.DeliverableGanttTasks.Commands
{
    public static class UpdateDeliverableGanttTaskEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.DeliverableGanttTasks.EndPoint.UpdateAll, async (DeliverableGanttTaskResponseList Data, IRepository Repository) =>
                {
                    await RemoveRows(Data, Repository);

                    await UpdateRows(Data, Repository);



                    var cache = StaticClass.DeliverableGanttTasks.Cache.GetAll(Data.ProjectId);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache);

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
            async Task RemoveRows(DeliverableGanttTaskResponseList Data, IRepository Repository)
            {
                Func<IQueryable<Deliverable>, IIncludableQueryable<Deliverable, object>> includes = x => x
               .Include(x => x.NewGanttTasks).ThenInclude(x => x.SubTasks);

                Expression<Func<Deliverable, bool>> criteria = x => x.ProjectId == Data.ProjectId;
                var deliverables = await Repository.GetAllAsync(Criteria: criteria, Includes: includes);

                var deliverablesDTO = Data.Items.Where(x => x.IsDeliverable == true).ToList();
                var tasksDTO = Data.Items.Where(x => x.IsTask == true).ToList();
                foreach (var deliverable in deliverables)
                {
                    if (!deliverablesDTO.Any(x => x.Id == deliverable.Id))
                    {
                        await Repository.RemoveAsync(deliverable);
                    }
                    if (deliverable.NewGanttTasks.Any())
                    {
                        await RemoveSubTask(tasksDTO, deliverable.NewGanttTasks, Repository);
                    }

                }
            }
            async Task RemoveSubTask(List<DeliverableGanttTaskResponse> Data, List<NewGanttTask> subTasks, IRepository Repository)
            {
                foreach (var task in subTasks)
                {
                    if (!Data.Any(x => x.Id == task.Id))
                    {
                        await Repository.RemoveAsync(task);
                    }
                    if (task.SubTasks.Any())
                    {
                        await RemoveSubTask(Data, task.SubTasks, Repository);
                    }
                }
            }
            async Task UpdateRows(DeliverableGanttTaskResponseList Data, IRepository Repository)
            {
                foreach (var data in Data.OrderedItems)
                {
                    if (data.IsDeliverable)
                    {
                        if (data.Id == Guid.Empty)
                        {
                            await CreateDeliverable(data, Data.ProjectId, Repository);
                        }
                        else
                        {
                            await UpdateDeliverable(data, Repository);

                        }
                    }
                    if (data.IsTask)
                    {
                        if (data.Id == Guid.Empty)
                        {
                            await CreateTask(data, Repository);
                        }
                        else
                        {
                            await UpdateTask(data, Repository);
                        }
                    }

                }
            }
            async Task CreateDeliverable(DeliverableGanttTaskResponse data, Guid ProjectId, IRepository Repository)
            {
                Deliverable? row = null!;
                var lastorder = await Repository.GetLastOrderAsync<Deliverable, Project>(ProjectId);
                row = Deliverable.Create(ProjectId, lastorder);
                data.MapDeliverable(row);

                await Repository.AddAsync(row);
            }
            async Task CreateTask(DeliverableGanttTaskResponse data, IRepository Repository)
            {
                NewGanttTask? row = null!;
                if (data.TaskParentId.HasValue)
                {
                    if (data.IsParentDeliverable)
                    {
                        row = NewGanttTask.Create(data.DeliverableId);
                    }
                    else
                    {
                        row = NewGanttTask.AddSubTask(data.TaskParentId.Value, data.DeliverableId);
                    }
                    await Repository.AddAsync(row);
                    data.MapTask(row);
                }


            }

            async Task UpdateDeliverable(DeliverableGanttTaskResponse data, IRepository Repository)
            {
                var deliverable = await Repository.GetByIdAsync<Deliverable>(data.Id);
                if (deliverable == null) { return; }
                data.MapDeliverable(deliverable);
                await Repository.UpdateAsync(deliverable);
            }
            async Task UpdateTask(DeliverableGanttTaskResponse data, IRepository Repository)
            {
                var task = await Repository.GetByIdAsync<NewGanttTask>(data.Id);
                if (task == null) { return; }
                data.MapTask(task);
                await Repository.UpdateAsync(task);
            }

        }


        static Deliverable MapDeliverable(this DeliverableGanttTaskResponse request, Deliverable row)
        {
            row.Name = request.Name;
            row.InternalOrder = request.InternalOrder;
            row.MainOrder = request.MainOrder;
            row.Name = request.Name;
            row.EndDate = request.StartDate;
            row.StartDate= request.EndDate;
            row.DurationInUnit = request.DurationInUnit;
            row.DurationInDays = request.DurationInDays;
            row.DurationUnit = request.DurationUnit;

            return row;
        }
        static NewGanttTask MapTask(this DeliverableGanttTaskResponse response, NewGanttTask row)
        {
            row.DependencyType = response.DependencyType.Id;
            row.Name = response.Name;
            row.InternalOrder = response.InternalOrder;
            row.Name = response.Name;
            row.MainOrder = response.MainOrder;
            row.StartDate = response.StartDate;
            row.EndDate= response.EndDate;
            row.LagInDays = response.LagInDays;
            row.DurationInDays = response.DurationInDays;
            row.LagUnit = response.LagUnit;
            row.DurationUnit= response.DurationUnit;
            row.DurationInUnit = response.DurationInUnit;
            row.LagInUnits = response.LagInUnits;
            row.ParentWBS = response.ParentWBS;
            row.ParentId = response.IsParentDeliverable ? null : response.TaskParentId;
            row.DependencyList = response.DependencyList;
            return row;
        }


    }
}
