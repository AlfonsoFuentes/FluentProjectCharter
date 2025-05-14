using Shared.Enums.TasksRelationTypes;
using Shared.Models.NewDeliverablesGanttTask.NewGanttTask.Responses;

namespace Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Responses
{
    public class NewDeliverableGanttTaskRow
    {
        public override string ToString()
        {
            return $"{WBS}.{Name}";
        }
        public NewDeliverableResponse Deliverable { get; set; } = null!;
        public NewTaskResponse Task { get; set; } = null!;
        public string Name
        {
            get { return IsDeliverable ? Deliverable.Name : Task.Name; }
            set
            {
                if (IsDeliverable)
                    Deliverable.Name = value;
                else
                    Task.Name = value;
            }
        }
        public bool HasDependency => IsTask ? Task.HasDependency : false;
        public Guid ProjectId { get; set; }
        public Guid? DeliverableId => Deliverable == null ? null : Deliverable.Id;
        public Guid? TaskId => Task == null ? null : Task.Id;
        public Guid? ParentTaskId => Task == null ? null : Task.TaskParentId;

        public int MainOrder => IsDeliverable ? Deliverable.MainOrder : Task.MainOrder;
        public int InternalOrder => IsDeliverable ? Deliverable.InternalOrder : Task.InternalOrder;
        public string WBS => IsDeliverable ? Deliverable.WBS : Task.WBS;
        public DateTime? PlannedStartDate
        {
            get { return IsDeliverable ? Deliverable.PlannedStartDate : Task.PlannedStartDate; }
            set
            {
                if (IsDeliverable)
                {
                    Deliverable.PlannedStartDate = value;
                    return;
                }

                Task.PlannedStartDate = value;
            }
        }
        public DateTime? PlannedEndDate
        {
            get { return IsDeliverable ? Deliverable.PlannedEndDate : Task.PlannedEndDate; }
            set
            {
                if (IsDeliverable)
                {
                    Deliverable.PlannedEndDate = value;
                    return;
                }
                Task.PlannedEndDate = value;
            }
        }
        public string? Duration
        {
            get { return IsDeliverable ? string.Empty : Task.Duration; }
            set
            {
                if (IsDeliverable)
                    return;
                Task.Duration = value;
            }
        }
        public TasksRelationTypeEnum DependencyType
        {
            get { return IsDeliverable ? TasksRelationTypeEnum.None : Task.DependencyType; }
            set
            {
                if (IsDeliverable)
                    return;
                Task.DependencyType = value;
            }
        }
        public string? DependencyList
        {
            get { return IsDeliverable ? string.Empty : Task.DependencyList; }
            set
            {
                if (IsDeliverable)
                    return;
                Task.DependencyList = value;
            }
        }
        public string? Lag
        {
            get { return IsDeliverable ? string.Empty : Task.Lag; }
            set
            {
                if (IsDeliverable)
                    return;
                Task.Lag = value;
            }
        }
        public string stringStart => PlannedStartDate == null ? string.Empty : PlannedStartDate.Value.ToString("d");
        public string stringEnd => PlannedEndDate == null ? string.Empty : PlannedEndDate.Value.ToString("d");
        public bool IsDeliverable => Deliverable != null && Task == null;
        public bool IsTask => Task != null;
        public bool IsCreatingTask => !IsTask ? false : TaskId == null ? true :
            TaskId.HasValue && TaskId.Value == Guid.Empty;
        public bool IsCreatingDeliverable => !IsDeliverable ? false : DeliverableId == null ? true :
            DeliverableId.HasValue && DeliverableId.Value == Guid.Empty;

        public bool IsCreating => IsCreatingTask || IsCreatingDeliverable;
    }
}
