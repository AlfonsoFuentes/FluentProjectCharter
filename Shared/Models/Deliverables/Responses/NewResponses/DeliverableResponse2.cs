using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.FileResults.Generics.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Deliverables.Responses.NewResponses
{
    public class DeliverableResponse : BaseResponse, IUpdateStateResponse
    {
        public override string ToString()
        {
            return $"{Name}-{StartDate}-{EndDate}-{Duration}";
        }
        public string EndPointName => StaticClass.Deliverables.EndPoint.UpdateState;
        public Guid ProjectId { get; set; }
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }

        public int LabelOrder { get; set; }
        public string WBS { get; set; } = string.Empty;
        public Guid? ParentDeliverableId { get; set; } // Referencia al padre (opcional)
        public List<DeliverableResponse> SubDeliverables { get; set; } = new();
        public List<DeliverableResponse> OrderedSubDeliverables => SubDeliverables.Count == 0 ? new() : SubDeliverables.OrderBy(x => x.LabelOrder).ToList();
        public Guid? DependantId { get; set; } // Referencia al padre (opcional)
        public List<DeliverableResponse> Dependants { get; set; } = new(); // Colección de subtareas
        public List<DeliverableResponse> OrderedDependants => Dependants.Count == 0 ? new() : Dependants.OrderBy(x => x.LabelOrder).ToList();
        public string sDependences => string.Join(",", OrderedDependants.Select(d => d.LabelOrder.ToString()));
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TasksRelationTypeEnum DependencyType { get; set; } = TasksRelationTypeEnum.None;
        public string? Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                SetDurationByUI(value);
            }

        }
        string _unitDuration { get; set; } = string.Empty;
        int _durationInDays = 0;
        string _duration = string.Empty;
        public void SetDurationByUI(string? rawinput)
        {

            if (string.IsNullOrWhiteSpace(rawinput))
            {
                return;
            }
            var input = rawinput.Trim();

            var match = System.Text.RegularExpressions.Regex.Match(input, @"^(\d+)\s*([dwmy]?)$");
            if (!match.Success)
            {
                return;
            }
            if (!int.TryParse(match.Groups[1].Value, out var numericValue) || numericValue < 0)
            {
                return;
            }
            _durationInDays = 0;
            _unitDuration = match.Groups[2].Value.ToLower();
            _duration = rawinput;
            switch (_unitDuration)
            {
                case "d":
                    _durationInDays = numericValue;
                    break;
                case "w":
                    _durationInDays = numericValue * 7;
                    break;
                case "m":
                    _durationInDays = numericValue * 30;
                    break;
                case "y":
                    _durationInDays = numericValue * 365;
                    break;
                case "":
                    _durationInDays = numericValue;
                    break;
                default:
                    return;
            }

        }
        public void SetDurationInDays(int durationindays)
        {
            _durationInDays = durationindays;
            var result = 0.0;
            switch (_unitDuration)
            {
                case "d": result = _durationInDays; break;
                case "w": result = _durationInDays / 7; break;
                case "m": result = _durationInDays / 30.44; break;
                case "y": result = _durationInDays / 365.24; break;
                default: return;
            }
            _duration = $"{result}{_unitDuration}";
        }
        
        public void CalculateDurationFromDates()
        {
            if (StartDate.HasValue && EndDate.HasValue)
            {
                SetDurationInDays(StartDate.Value.Day - EndDate.Value.Day);
            }
        }

        public void AddSubDeliverable(DeliverableResponse subDeliverable)
        {
            subDeliverable.ParentDeliverableId = ParentDeliverableId;
            SubDeliverables.Add(subDeliverable);
        }
        public void RemoveSubDeliverable(DeliverableResponse subDeliverable)
        {
            if (SubDeliverables.Contains(subDeliverable))
            {
                subDeliverable.ParentDeliverableId = null;
                SubDeliverables.Remove(subDeliverable);
            }
        }
        public bool GetCircularReference(DeliverableResponse newCurrent = null!)
        {

            return false;
        }
       
        public void CalculateEndDate()
        {
            if (StartDate.HasValue)
            {
                EndDate=StartDate.Value.AddDays(_durationInDays);
            }
        }
        public void CalculateStartDate()
        {
            if (EndDate.HasValue)
            {
                StartDate = EndDate.Value.AddDays(-_durationInDays);
            }
        }
        public void CalculateWithoutDependencesOrDeliverables()
        {
            CalculateEndDate();
        }
    }
}
