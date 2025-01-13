using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Taxs.Requests;

namespace FluentWeb.Pages.BudgetItems.Taxs;
public partial class CreateTax
{
    CreateTaxRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid DeliverableId { get; set; }
    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;
        Model.DeliverableId = DeliverableId;
    }
   
}
