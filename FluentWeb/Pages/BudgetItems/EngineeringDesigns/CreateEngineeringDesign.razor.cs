using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.EngineeringDesigns.Requests;

namespace FluentWeb.Pages.BudgetItems.EngineeringDesigns;
public partial class CreateEngineeringDesign
{
    CreateEngineeringDesignRequest Model = new();
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
