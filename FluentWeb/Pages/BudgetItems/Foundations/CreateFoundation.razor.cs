using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Foundations.Requests;

namespace FluentWeb.Pages.BudgetItems.Foundations;
public partial class CreateFoundation
{
    CreateFoundationRequest Model = new();
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
