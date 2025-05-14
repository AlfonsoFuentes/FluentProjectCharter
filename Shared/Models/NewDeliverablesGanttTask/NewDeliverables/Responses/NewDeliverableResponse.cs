using Shared.Enums.TasksRelationTypes;
using Shared.Models.NewDeliverablesGanttTask.NewGanttTask.Responses;

namespace Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Responses
{
    public class NewDeliverableResponse
    {
        public override string ToString()
        {
            return $"{WBS}-{Name}";
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid ProjectId { get; set; } = Guid.Empty;

        public string WBS => $"{InternalOrder}";
        public int MainOrder { get; set; }
        public int InternalOrder { get; set; }
        DateTime? _PlannedStartDate;
        public DateTime? PlannedStartDate
        {
            get
            {
                if(SubTasks.Any())
                {
                    _PlannedStartDate = SubTasks.Min(x => x.PlannedStartDate);
                }
                return _PlannedStartDate;
            }
            set
            {
                _PlannedStartDate = value;
            }
        }
        DateTime? _PlannedEndDate;
        public DateTime? PlannedEndDate
        {
            get
            {
                if (SubTasks.Any())
                {
                    _PlannedEndDate = SubTasks.Max(x => x.PlannedEndDate);
                }
                return _PlannedEndDate;
            }
            set
            {
                _PlannedEndDate = value;
            }
        }
        public string? Duration { get; set; } = string.Empty;
        public List<NewTaskResponse> SubTasks { get; set; } = new();
        public List<NewTaskResponse> OrderedSubTasks => SubTasks.OrderBy(x => x.InternalOrder).ToList();
        public int LastInternalOrder => OrderedSubTasks.Count == 0 ? 0 : OrderedSubTasks.Last().InternalOrder;
        public int LastMainOrder => OrderedSubTasks.Count == 0 ? 0 : OrderedSubTasks.Last().MainOrder;
        public int FirstMainOrder => OrderedSubTasks.Count == 0 ? 0 : OrderedSubTasks.First().MainOrder;
        public List<NewTaskResponse> FlatDeliverableTasks => GetFlatTask(OrderedSubTasks);
        public List<NewTaskResponse> OrderedFlatDeliverableTasks => FlatDeliverableTasks.OrderBy(x => x.MainOrder).ToList();

        List<NewTaskResponse> GetFlatTask(List<NewTaskResponse> orderedDeliverableTasks)
        {
            List<NewTaskResponse> result = new();
            foreach (var task in orderedDeliverableTasks)
            {
                result.Add(task);

                if (task.SubTasks != null && task.SubTasks.Count > 0)
                {
                    var subTasks = GetFlatTask(task.OrderedSubTasks);
                    result.AddRange(subTasks);
                }

            }

            return result;
        }
    }
}
