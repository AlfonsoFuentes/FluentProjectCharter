using Shared.Enums.TasksRelationTypes;
using Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Responses;
using Shared.Models.NewDeliverablesGanttTask.NewGanttTask.Responses;

namespace Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Helpers
{
    public static class NewDeliverableHelper
    {
        public static NewDeliverableGanttTaskRow GetRowFromMainOrder(this NewDeliverablesResponseList response, string mainorder)
        {

            return response.OrderedFlatenList.FirstOrDefault(x => x.MainOrder.ToString() == mainorder) ?? null!;
        }
        public static List<NewDeliverableGanttTaskRow> FlattenList(this List<NewDeliverableResponse> Deliverables)
        {
            List<NewDeliverableGanttTaskRow> result = new();

            foreach (var deliverable in Deliverables)
            {
                result.Add(deliverable.Map());
                foreach (var task in deliverable.SubTasks)
                {
                    result.Add(task.Map(deliverable));
                    if (task.SubTasks.Any())
                    {
                        result.AddRange(task.FlatenList(deliverable));
                    }
                }
            }

            return result;

        }
        public static List<NewDeliverableGanttTaskRow> FlatenList(this NewTaskResponse ParenTask, NewDeliverableResponse deliverable)
        {
            List<NewDeliverableGanttTaskRow> result = new();

            foreach (var task in ParenTask.SubTasks)
            {
                result.Add(task.Map(deliverable));
                if (task.SubTasks.Any())
                {
                    result.AddRange(task.FlatenList(deliverable));
                }
            }
            return result;

        }
        public static NewDeliverableResponse GetDeliverable(this NewDeliverablesResponseList response, Guid DeliverableId)
        {
            return response.Deliverables.FirstOrDefault(x => x.Id == DeliverableId) ?? new NewDeliverableResponse();
        }
        public static NewTaskResponse GetDeliverableTask(this NewDeliverablesResponseList response, Guid DeliverableTaskId)
        {
            return response.GanttTasks.FirstOrDefault(x => x.Id == DeliverableTaskId) ?? new NewTaskResponse();
        }
        public static NewDeliverableGanttTaskRow? GetDeliverableTaskFromOverallList(this NewDeliverablesResponseList response, Guid? Id, bool SearchByTask)
        {
            if (SearchByTask)
                return response.TaskFlatenList.FirstOrDefault(x => x.TaskId == Id) ?? null!;
            return response.DeliverableFlatenList.FirstOrDefault(x => x.DeliverableId == Id) ?? null!;

        }
        public static NewDeliverableGanttTaskRow? CreateDeliverable(this NewDeliverablesResponseList response)
        {
            var result = new NewDeliverableResponse()
            {
                InternalOrder = response.LastInternalOrder + 1,
                MainOrder = response.LastMainOrder + 1,
                ProjectId = response.ProjectId,
                PlannedStartDate = response.InitialProjectStartDate,
                Duration = "1d",
                PlannedEndDate = response.InitialProjectStartDate?.AddDays(1),

            };

            response.Deliverables.Add(result);
            var request = response.OrderedFlatenList.FirstOrDefault(x => x.IsCreatingDeliverable == true);
            return request;
        }
        public static NewDeliverableGanttTaskRow? CreateTask(this NewDeliverablesResponseList response, NewDeliverableGanttTaskRow selected)
        {
            var result = response.AddSubTask(selected);
            response.ReOrder();
            var request = response.OrderedFlatenList.FirstOrDefault(x => x.IsCreatingTask == true);
            return request;
        }
        public static void AddSubTask(this NewTaskResponse task, NewTaskResponse subTask)
        {
            subTask.TaskParentId = task.Id;
            subTask.InternalOrder = task.LastInternalOrder + 1;

            task.SubTasks.Add(subTask);

        }
        public static void AddSubtask(this NewDeliverableResponse deliverable, NewTaskResponse subTask)
        {
            subTask.DeliverableId = deliverable.Id; // Establecer la referencia al padre
            subTask.InternalOrder = deliverable.LastInternalOrder + 1; // Asignar un nuevo orden
            subTask.ProjectId = deliverable.ProjectId; // Asignar el ProjectId
            deliverable.SubTasks.Add(subTask);

        }
        static NewTaskResponse AddSubTask(this NewDeliverablesResponseList Response,
            NewDeliverableGanttTaskRow selectedRow)
        {
            NewTaskResponse subTask = new()
            {
                PlannedStartDate = Response.InitialProjectStartDate,
                DurationDays = 1,
                DurationUnits = "d",
                PlannedEndDate = Response.InitialProjectStartDate?.AddDays(1),
                LagDays = 0,
                LagUnits = "d",
                DependencyType = TasksRelationTypeEnum.None,
                ProjectId = Response.ProjectId,
            };

            if (selectedRow.TaskId.HasValue)
            {
                var selectedTask = Response.GetDeliverableTask(selectedRow.TaskId.Value);
                selectedTask.AddSubTask(subTask);
                subTask.TaskParentId = selectedTask.Id;

            }
            else if (selectedRow.DeliverableId.HasValue)
            {
                var selectedDeliverable = Response.GetDeliverable(selectedRow.DeliverableId.Value);
                selectedDeliverable.AddSubtask(subTask);
                subTask.DeliverableId = selectedDeliverable.Id;


            }

            return subTask;
        }


        public static void ReOrder(this NewDeliverablesResponseList response)
        {
            int labelorder = 1;
            foreach (var deliverable in response.OrderedDeliverables)
            {
                deliverable.MainOrder = labelorder;

                foreach (var task in deliverable.OrderedSubTasks)
                {
                    labelorder++;
                    task.MainOrder = labelorder;
                    task.ParentWBS = deliverable.WBS;

                    if (task.OrderedSubTasks.Any())
                        ReOrderTask(task, ref labelorder);

                }
                labelorder++;
            }

        }

        public static void ReOrderTask(NewTaskResponse parenTask, ref int labelorder)
        {

            foreach (var task in parenTask.OrderedSubTasks)
            {
                labelorder++;
                task.MainOrder = labelorder;
                task.ParentWBS = parenTask.WBS;
                if (task.OrderedSubTasks.Any())
                    ReOrderTask(task, ref labelorder);
            }
        }
        public static void Delete(this NewDeliverablesResponseList Response, NewDeliverableGanttTaskRow selectedRow)
        {
            if (selectedRow.IsDeliverable)
            {
                var selectedDeliverable = Response.GetDeliverable(selectedRow.Deliverable.Id);
                Response.Deliverables.Remove(selectedDeliverable);
            }
            else if (selectedRow.IsTask)
            {
                if (selectedRow.ParentTaskId.HasValue && selectedRow.ParentTaskId.Value != Guid.Empty)
                {
                    var selectedTask = Response.GetDeliverableTask(selectedRow.ParentTaskId.Value);

                    var tastoCancel = selectedTask.SubTasks.FirstOrDefault(x => x.Id == selectedRow.Task.Id);
                    if (tastoCancel != null)
                        selectedTask.RemoveSubTask(tastoCancel);
                }
                else if (selectedRow.DeliverableId.HasValue)
                {
                    var selectedDeliverable = Response.GetDeliverable(selectedRow.DeliverableId.Value);
                    var selectedTask = selectedDeliverable.SubTasks.FirstOrDefault(x => x.Id == selectedRow.Task.Id);
                    if (selectedTask != null)
                        selectedDeliverable.RemoveSubTask(selectedTask);
                }
            }


        }
        public static void CancelCreate(this NewDeliverablesResponseList Response, NewDeliverableGanttTaskRow selectedRow)
        {
            if (selectedRow.IsDeliverable)
            {
                var tastoCancel = Response.Deliverables.FirstOrDefault(x => x.Id == Guid.Empty);
                if (tastoCancel != null)
                    Response.Deliverables.Remove(tastoCancel);
            }
            else if (selectedRow.IsTask)
            {
                if (selectedRow.ParentTaskId.HasValue)
                {
                    var selectedTask = Response.GetDeliverableTask(selectedRow.ParentTaskId.Value);

                    var tastoCancel = selectedTask.SubTasks.FirstOrDefault(x => x.Id == Guid.Empty);
                    if (tastoCancel != null)
                        selectedTask.RemoveSubTask(tastoCancel);
                }
                else
                {
                    var selectedDeliverable = Response.GetDeliverable(selectedRow.Deliverable.Id);
                    var selectedTask = selectedDeliverable.SubTasks.FirstOrDefault(x => x.Id == Guid.Empty);
                    if (selectedTask != null)
                        selectedDeliverable.RemoveSubTask(selectedTask);
                }
            }


        }

    }
}
