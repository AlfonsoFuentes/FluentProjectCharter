using Shared.Models.BudgetItems.Alterations.Responses;
using Shared.Models.BudgetItems.EHSs.Responses;
using Shared.Models.BudgetItems.Electricals.Responses;
using Shared.Models.BudgetItems.EngineeringDesigns.Responses;
using Shared.Models.BudgetItems.Equipments.Responses;
using Shared.Models.BudgetItems.Foundations.Responses;
using Shared.Models.BudgetItems.Instruments.Responses;
using Shared.Models.BudgetItems.Paintings.Responses;
using Shared.Models.BudgetItems.Pipes.Responses;
using Shared.Models.BudgetItems.Structurals.Responses;
using Shared.Models.BudgetItems.Taxs.Requests;
using Shared.Models.BudgetItems.Testings.Responses;
using Shared.Models.BudgetItems.Valves.Responses;
using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.BudgetItems.Taxs.Responses
{
    public class TaxItemResponseList : IResponseAll
    {
        public List<TaxItemResponse> Items { get; set; } = new();



    }
}
