using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Alterations.Requests;

namespace FluentWeb.Pages.BudgetItems.Alterations;
public partial class CreateAlteration
{
    CreateAlterationRequest Model = new();
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
