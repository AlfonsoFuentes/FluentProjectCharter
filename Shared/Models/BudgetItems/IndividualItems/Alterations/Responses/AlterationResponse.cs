using Shared.Enums.CostCenter;
using Shared.Models.BudgetItems.Responses;

namespace Shared.Models.BudgetItems.IndividualItems.Alterations.Responses
{
    public class AlterationResponse : BudgetItemWithPurchaseOrdersResponse
    {

        public Guid DeliverableId { get; set; }
      
        public override bool IsAlteration { get; set; } = true;
        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;
        double _UnitaryCost;
        double _Quantity;
        public double UnitaryCost
        {
            get { return _UnitaryCost; }
            set
            {
                _UnitaryCost = value;
                BudgetUSD = _Quantity * _UnitaryCost;
            }
        }
        public double Quantity
        {
            get { return _Quantity; }
            set
            {
                _Quantity = value;
                BudgetUSD = _Quantity * _UnitaryCost;
            }
        }



        public string sUnitaryCost => string.Format(new CultureInfo("en-US"), "{0:C0}", UnitaryCost);
        public string sQuantity => $"{Quantity}";
       

        public override string UpadtePageName { get; set; } = StaticClass.Alterations.PageName.Update;
     
       
    }
}
