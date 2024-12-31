namespace Server.Database.Entities.BudgetItems.Taxes
{
    public class Tax : BudgetItem
    {

        public override string Letter { get; set; } = "L";
        public double Percentage { get; set; }
        public ICollection<TaxesItem> TaxesItems { get; set; } = new List<TaxesItem>();
        public TaxesItem AddTaxItem(Guid SelectedItemId)
        {
            TaxesItem result = new TaxesItem();
            result.Id = Guid.NewGuid();
            result.TaxItemId = Id;
            result.SelectedId = SelectedItemId;
            TaxesItems.Add(result);
            return result;
        }
    }

}
