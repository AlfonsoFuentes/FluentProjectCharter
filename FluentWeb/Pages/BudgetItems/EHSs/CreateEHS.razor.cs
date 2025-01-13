using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.EHSs.Requests;

namespace FluentWeb.Pages.BudgetItems.EHSs;
public partial class CreateEHS
{
    CreateEHSRequest Model = new();
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
