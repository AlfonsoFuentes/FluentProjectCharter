using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Structurals.Requests;

namespace FluentWeb.Pages.BudgetItems.Structurals;
public partial class CreateStructural
{
    CreateStructuralRequest Model = new();
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
