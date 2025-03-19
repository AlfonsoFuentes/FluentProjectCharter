using Shared.Enums.TasksRelationTypes;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Responses;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Responses;
using Shared.Models.BudgetItems.IndividualItems.Electricals.Responses;
using Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Responses;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Responses;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Responses;
using Shared.Models.BudgetItems.IndividualItems.Instruments.Responses;
using Shared.Models.BudgetItems.IndividualItems.Paintings.Responses;
using Shared.Models.BudgetItems.IndividualItems.Pipes.Responses;
using Shared.Models.BudgetItems.IndividualItems.Structurals.Responses;
using Shared.Models.BudgetItems.IndividualItems.Taxs.Responses;
using Shared.Models.BudgetItems.IndividualItems.Testings.Responses;
using Shared.Models.BudgetItems.IndividualItems.Valves.Responses;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.GanttTasks.Responses
{
    public class GanttTaskResponse : BaseResponse, IUpdateStateResponse
    {
        public override string ToString()
        {
            return $"{Name}-{StartDate}-{EndDate}-{Duration}";
        }
        // Propiedad calculada para las líneas de texto
        private List<string> _textLines = null!;
        public List<string> TextLines(int maxWidth, int averageCharWidth = 7)
        {
            if (_textLines == null)
            {
                _textLines = TextHelper.SplitTextIntoLines($"{WBS} - {Name}", maxWidth, averageCharWidth);
            }
            return _textLines;
        }
        public string EndPointName => StaticClass.GanttTasks.EndPoint.UpdateState;
        public List<string> textLines = new(); // Líneas de texto divididas
        public Guid ProjectId { get; set; }
        public Guid DeliverableId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public int LabelOrder { get; set; }
        public string WBS { get; set; } = string.Empty;
        public Guid? ParentGanttTaskId { get; set; } // Referencia al padre (opcional)
        public List<GanttTaskResponse> SubGanttTasks { get; set; } = new();
        public List<GanttTaskResponse> OrderedSubGanttTasks => SubGanttTasks.Count == 0 ? new() : SubGanttTasks.OrderBy(x => x.Order).ToList();
        public int LastOrder => SubGanttTasks.Count == 0 ? 1 : OrderedSubGanttTasks.Last().Order;
        public Guid? DependantId { get; set; } // Referencia al padre (opcional)
        public List<GanttTaskResponse> Dependants { get; set; } = new(); // Colección de subtareas
        public List<GanttTaskResponse> OrderedDependants => Dependants.Count == 0 ? new() : Dependants.OrderBy(x => x.LabelOrder).ToList();
        public string sDependences => OrderedDependants.Count == 0 ? string.Empty : string.Join(",", OrderedDependants.Select(d => d.LabelOrder.ToString()));
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime EndDateValue => EndDate.HasValue ? EndDate.Value : DateTime.Now;
        public DateTime StartDateValue => StartDate.HasValue ? StartDate.Value : DateTime.Now;
        public string sEndDateValue => EndDateValue.ToString("d");
        public string sStartDateValue => StartDateValue.ToString("d");
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
        public int DurationInDays => _durationInDays;
        int _durationInDays = 0;
        string _duration = string.Empty;
        public void SetDuration(string? rawinput)
        {

            if (string.IsNullOrWhiteSpace(rawinput))
            {
                rawinput = "1d";
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

        public void AddSubGanttTask(GanttTaskResponse subGanttTask)
        {

            subGanttTask.ParentGanttTaskId = Id;
            subGanttTask.Order = LastOrder + 1;
            SubGanttTasks.Add(subGanttTask);
        }
        public void RemoveSubGanttTask(GanttTaskResponse subGanttTask)
        {
            if (SubGanttTasks.Contains(subGanttTask))
            {
                subGanttTask.ParentGanttTaskId = null;
                SubGanttTasks.Remove(subGanttTask);
            }
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
                _lagInDays = 0;
                _unitLag = string.Empty;
                _lag = string.Empty;
                return;
            }
            var input = rawinput.Trim();

            var match = System.Text.RegularExpressions.Regex.Match(input, @"^(\d+)\s*([dwmy]?)$");
            if (!match.Success)
            {
                _lagInDays = 0;
                _unitLag = string.Empty;
                _lag = string.Empty;
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


        public bool ShowBudgetItems { get; set; }
        public bool IsExpanded { get; set; } = true;
        public bool IsEditing { get; set; } = false;
        public bool IsCreating { get; set; } = false;
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
        public double TotalCapital => Capital.Sum(x => x.BudgetUSD) + TaxesBudget;

        public double TotalCapitalWithOutVAT => Capital.Sum(x => x.BudgetUSD);
        public double TotalExpenses => Expenses.Sum(x => x.BudgetUSD);
        public double PercentageEngineering { get; set; }
        public double PercentageContingency { get; set; }
        public double PercentageTaxes { get; set; }
        public bool IsProductive { get; set; } = true;

        public double EngineeringBudget => TotalCapital * PercentageEngineering / 100;
        public double ContingenyBudget => TotalCapital * PercentageContingency / 100;
        public double TaxesBudget => IsProductive ? 0 : TotalCapitalWithOutVAT * PercentageTaxes / 100;
        public double TotalBudget => TotalCapital + TotalExpenses + EngineeringBudget + ContingenyBudget;

        public string sTotalCapital => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalCapital);
        public string sTotalExpenses => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalExpenses);
        public string sEngineeringBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", EngineeringBudget);
        public string sContingenyBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", ContingenyBudget);
        public string sTotalBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalBudget);
        public string sTaxesBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", TaxesBudget);
        public string sTotalCapitalWithOutVAT => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalCapitalWithOutVAT);
    }
}
