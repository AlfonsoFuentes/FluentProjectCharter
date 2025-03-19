using Shared.Enums.BudgetItemTypes;
using Shared.Enums.CostCenter;
using Shared.Models.BudgetItems.Responses;
using System.Globalization;

namespace Shared.Models.BudgetItems.IndividualItems.Paintings.Responses
{
    public class PaintingResponse : BudgetItemWithPurchaseOrdersResponse
    {
   
        public Guid DeliverableId { get; set; }


  
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
     

        public override string UpadtePageName { get; set; } = StaticClass.Paintings.PageName.Update;
        

    }
}
