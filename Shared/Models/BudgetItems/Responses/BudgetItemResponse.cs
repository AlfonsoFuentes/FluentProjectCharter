using Shared.Models.BudgetItemNewGanttTasks.Responses;
using System.Text.Json.Serialization;

namespace Shared.Models.BudgetItems.Responses
{
    public class BudgetItemResponse : BaseResponse, IBudgetItemResponse
    {
        public bool ShowDetails { get; set; }
        public bool Selected { get; set; }
        public virtual double BudgetUSD { get; set; }
        public int OrderList { get; set; }
        public string Nomenclatore { get; set; } = string.Empty;
        public string NomenclatoreName => $"{Nomenclatore}-{Name}";
        public string Brand { get; set; } = string.Empty;
        public virtual string Tag { get; set; } = string.Empty;
        public virtual bool IsAlteration { get; set; }
        public bool IsTaxes { get; set; }
        public List<BudgetItemNewGanttTaskResponse> BudgetItemGanttTasks { get; set; } = new();
        [JsonIgnore]
        public double PercentageAssigned => BudgetItemGanttTasks.Sum(x => x.PercentageBudget);
        [JsonIgnore]
        public bool IsAvailableToAssignedToTask => PercentageAssigned < 100;

    }

}
