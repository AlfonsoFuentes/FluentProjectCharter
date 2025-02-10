using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.BudgetItems.Taxs.Responses
{
    public class TaxItemResponseList : IResponseAll
    {
        public List<TaxItemResponse> Items { get; set; } = new();



    }
}
