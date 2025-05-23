using Shared.Models.BudgetItems.Responses;
using System.Text.Json.Serialization;

namespace Shared.Models.BudgetItemNewGanttTasks.Responses
{
    public class BudgetItemNewGanttTaskResponse
    {
        BudgetItemResponse? _BudgetItem { get; set; }
        [JsonIgnore]
        public BudgetItemResponse? BudgetItem
        {
            get { return _BudgetItem; }
            set
            {
                _BudgetItem = value;
                if (_BudgetItem != null)
                {
                    _BudgetAssigned = _PercentageBudget * BudgetUSD / 100.0;

                }
            }
        }
        [JsonIgnore]
        public string NomenclatoreName => BudgetItem == null ? string.Empty : BudgetItem.NomenclatoreName;
        [JsonIgnore]
        public string Nomenclatore => BudgetItem == null ? string.Empty : BudgetItem.Nomenclatore;
        [JsonIgnore]
        public string Name => BudgetItem == null ? string.Empty : BudgetItem.Name;

        public double BudgetUSD => BudgetItem == null ? 0 : BudgetItem.BudgetUSD;
        [JsonIgnore]
        public double PercentageAssigned => BudgetItem == null ? 0 : BudgetItem.BudgetItemGanttTasks
            .Where(x => x.GanttTaskId != GanttTaskId).Sum(x => x.PercentageBudget);
        [JsonIgnore]
        public double PercentageAvailable => 100 - PercentageAssigned;
        public Guid GanttTaskId { get; set; }
        public Guid BudgetItemId { get; set; }
        double _PercentageBudget = 100;
        public double PercentageBudget
        {
            get => _PercentageBudget;
            set
            {
                if (value < 0)
                {
                    _PercentageBudget = 0;
                }
                else if (value > PercentageAvailable)
                {
                    _PercentageBudget = PercentageAvailable;
                }
                else
                {
                    _PercentageBudget = value;
                }
                _BudgetAssigned = _PercentageBudget * BudgetUSD / 100.0;
            }
        }

        [JsonIgnore]
        public double TotalBudgetAssignedByOther => BudgetItem == null ? 0 : BudgetItem.BudgetItemGanttTasks
            .Where(x => x.GanttTaskId != GanttTaskId).Sum(x => x.BudgetAssignedUSD);
        [JsonIgnore]
        public double PendingToAssign => BudgetUSD - TotalBudgetAssignedByOther - _BudgetAssigned;

        double _BudgetAssigned;
        public double BudgetAssignedUSD
        {
            get => _BudgetAssigned;
            set => _BudgetAssigned = value;
        }
        public int Order { get; set; } = 0;
    }

}
