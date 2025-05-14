using Shared.Enums.TasksRelationTypes;
using Shared.Models.NewDeliverablesGanttTask.NewGanttTask.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DeliverableGanttTasks.Responses
{
    public class DeliverableGanttTaskResponse
    {
        public override string ToString()
        {
            return $"{WBS}{Name}";
        }
        public bool IsCreating => Id == Guid.Empty;
        public Guid Id { get; set; } = Guid.Empty;
        public bool IsDeliverable { get; set; } = false;
        public bool IsParentDeliverable { get; set; } = false;
        public bool IsTask => !IsDeliverable;
        public int MainOrder { get; set; } = 0;
        public int InternalOrder { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string ParentWBS { get; set; } = string.Empty;
        public string WBS => string.IsNullOrEmpty(ParentWBS) ? $"{InternalOrder}" : $"{ParentWBS}.{InternalOrder}";
        DateTime? _StartDate;
        DateTime? _EndDate;

        public string stringStartDate => StartDate == null ? string.Empty : StartDate.Value.ToString("d");
        public string stringEndDate => EndDate == null ? string.Empty : EndDate.Value.ToString("d");
        public DateTime? StartDate
        {
            get
            {
                if (IsCalculated)
                {
                    _StartDate = this.GetCalculatedStartDate();
                }


                return _StartDate;
            }
            set
            {
                _StartDate = value;

            }
        }
        public DateTime? EndDate
        {
            get
            {
                if (IsCalculated)
                {
                    _EndDate = this.GetCalculateEndDate();
                }
                else
                {
                    _EndDate = StartDate?.AddDays(DurationInDays);
                }

                return _EndDate;
            }
            set
            {
                _EndDate = value;
            }
        }
        public string? Duration
        {
            get
            {
                return this.GetDuration();
            }
            set
            {
                this.SetDuration(value);

            }
        }
        public string? DurationUnit { get; set; } = string.Empty;
        public double DurationInDays { get; set; }
        public double DurationInUnit { get; set; }

        public string? LagUnit { get; set; } = string.Empty;
        public double LagInDays { get; set; }
        public double LagInUnits { get; set; }
        public string? Lag
        {
            get { return this.GetLag(); }
            set { this.SetLag(value); }
        }
        string? _DependencyList;
        public string? LoadDependencyList { get; set; }
        public string? DependencyList
        {
            get
            {
                _DependencyList = this.GetDependencyList();
                return _DependencyList;
            }
            set { _DependencyList = value; }
        }
        public TasksRelationTypeEnum DependencyType { get; set; } = TasksRelationTypeEnum.None;
        public Guid? TaskParentId { get; set; } // Referencia al padre (opcional)
        public Guid DeliverableId { get; set; } // Referencia al deliverable
        public bool HasSubTask => SubTasks.Any();
        public bool HasDependencies => Dependencies.Any();
        public bool IsCalculated => HasDependencies || HasSubTask;
        public List<DeliverableGanttTaskResponse> SubTasks { get; set; } = new(); // Colección de subtareas
        public List<DeliverableGanttTaskResponse> OrderedSubTasks => SubTasks.OrderBy(x => x.InternalOrder).ToList();
        public int LastInternalOrder => OrderedSubTasks.Count == 0 ? 0 : OrderedSubTasks.Last().InternalOrder;
        public int LastMainOrder => OrderedSubTasks.Count == 0 ? 0 : OrderedSubTasks.Last().MainOrder;
        public int FirstMainOrder => OrderedSubTasks.Count == 0 ? 0 : OrderedSubTasks.First().MainOrder;
        public List<DeliverableGanttTaskResponse> Dependencies { get; set; } = new();

    }
}
