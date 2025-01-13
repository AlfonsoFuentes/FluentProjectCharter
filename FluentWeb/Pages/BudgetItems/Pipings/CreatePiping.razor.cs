using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Pipings.Requests;

namespace FluentWeb.Pages.BudgetItems.Pipings;
public partial class CreatePiping
{
    CreatePipingRequest Model = new();
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
