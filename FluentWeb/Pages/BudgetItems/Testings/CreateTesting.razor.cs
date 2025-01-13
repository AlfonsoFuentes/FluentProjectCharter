using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Testings.Requests;

namespace FluentWeb.Pages.BudgetItems.Testings;
public partial class CreateTesting
{
    CreateTestingRequest Model = new();
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
