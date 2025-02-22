using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Responses;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Responses;
using Shared.Models.BudgetItems.IndividualItems.Electricals.Responses;
using Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Responses;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Responses;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Responses;
using Shared.Models.BudgetItems.IndividualItems.Instruments.Responses;
using Shared.Models.BudgetItems.IndividualItems.Paintings.Responses;
using Shared.Models.BudgetItems.IndividualItems.Pipes.Responses;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.BudgetItems.Structurals.Responses;
using Shared.Models.BudgetItems.Taxs.Responses;
using Shared.Models.BudgetItems.Testings.Responses;
using Shared.Models.BudgetItems.Valves.Responses;
using Shared.Models.FileResults.Generics.Reponses;

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
        public string ProjectName { get; set; } = string.Empty;
        public int LabelOrder { get; set; }
        public string WBS { get; set; } = string.Empty;
        public Guid? ParentDeliverableId { get; set; } // Referencia al padre (opcional)
        public List<DeliverableResponse> SubDeliverables { get; set; } = new();
        public List<DeliverableResponse> OrderedSubDeliverables => SubDeliverables.Count == 0 ? new() : SubDeliverables.OrderBy(x => x.Order).ToList();
        public int LastOrder => SubDeliverables.Count == 0 ? 1 : OrderedSubDeliverables.Last().Order;
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
                SetDuration(value);
            }

        }
        string _unitDuration { get; set; } = string.Empty;
        int _durationInDays = 0;
        string _duration = string.Empty;
        public void SetDuration(string? rawinput)
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
            if (!StartDate.HasValue || !EndDate.HasValue)
            {
                return;
            }

            TimeSpan difference = EndDate.Value - StartDate.Value;


            SetDurationInDays(difference.Days);

        }

        public void AddSubDeliverable(DeliverableResponse subDeliverable)
        {

            subDeliverable.ParentDeliverableId = Id;
            subDeliverable.Order = LastOrder + 1;
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

        public void SetStartDateWithLag(DateTime date)
        {
            if (string.IsNullOrEmpty(Lag))
            {
                StartDate = date;
            }
            else
            {
                StartDate = date.AddDays(_lagInDays);
            }
        }
        public void SetEndDateWithlag(DateTime date)
        {
            if (string.IsNullOrEmpty(Lag))
            {
                EndDate = date;
            }
            else
            {
                EndDate = date.AddDays(_lagInDays);
            }
        }
        public void CalculateEndDate()
        {
            if (StartDate.HasValue)
            {
                EndDate = StartDate.Value.AddDays(_durationInDays);
            }
        }
        public void CalculateStartDate()
        {
            if (EndDate.HasValue)
            {
                StartDate = EndDate.Value.AddDays(-_durationInDays);
            }
        }

        public string? Lag
        {
            get
            {
                return _lag;
            }
            set
            {
                SetLag(value);
            }

        }
        string _unitLag { get; set; } = string.Empty;
        int _lagInDays = 0;
        string _lag = string.Empty;
        public void SetLag(string? rawinput)
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
            _lagInDays = 0;
            _unitLag = match.Groups[2].Value.ToLower();
            _lag = rawinput;
            switch (_unitLag)
            {
                case "d":
                    _lagInDays = numericValue;
                    break;
                case "w":
                    _lagInDays = numericValue * 7;
                    break;
                case "m":
                    _lagInDays = numericValue * 30;
                    break;
                case "y":
                    _lagInDays = numericValue * 365;
                    break;
                case "":
                    _lagInDays = numericValue;
                    break;
                default:
                    return;
            }

        }
        public void SetLagInDays(int lagindays)
        {
            _lagInDays = lagindays;
            var result = 0.0;
            switch (_unitDuration)
            {
                case "d": result = _lagInDays; break;
                case "w": result = _lagInDays / 7; break;
                case "m": result = _lagInDays / 30.44; break;
                case "y": result = _lagInDays / 365.24; break;
                default: return;
            }
            _duration = $"{result}{_unitLag}";
        }
        public List<AlterationResponse> Alterations { get; set; } = new();
        public List<FoundationResponse> Foundations { get; set; } = new();
        public List<StructuralResponse> Structurals { get; set; } = new();
        public List<EquipmentResponse> Equipments { get; set; } = new();
        public List<ElectricalResponse> Electricals { get; set; } = new();
        public List<PipeResponse> Pipings { get; set; } = new();
        public List<InstrumentResponse> Instruments { get; set; } = new();
        public List<EHSResponse> EHSs { get; set; } = new();
        public List<PaintingResponse> Paintings { get; set; } = new();
        public List<TaxResponse> Taxes { get; set; } = new();
        public List<TestingResponse> Testings { get; set; } = new();
        public List<ValveResponse> Valves { get; set; } = new();
        public List<EngineeringDesignResponse> EngineeringDesigns { get; set; } = new();
        public List<IBudgetItemResponse> Expenses => [.. Alterations];
        public List<IBudgetItemResponse> Capital => [..Foundations,..Structurals,..Equipments,..Valves,..Electricals,
            ..Pipings,..Instruments,..EHSs,..Paintings,..Taxes,..Testings,..EngineeringDesigns];

        public List<IBudgetItemResponse> Items => BudgetItems.OrderBy(x => x.Nomenclatore).ToList();
        public List<IBudgetItemResponse> BudgetItems => [.. Expenses, .. Capital];

        public bool ShowBudgetItems {  get; set; }
    }
}
