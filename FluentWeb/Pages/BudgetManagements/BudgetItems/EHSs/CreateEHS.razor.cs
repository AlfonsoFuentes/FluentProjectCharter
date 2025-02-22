using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Requests;
#nullable disable
namespace FluentWeb.Pages.BudgetManagements.BudgetItems.EHSs;
public partial class CreateEHS
{
    [CascadingParameter]
    public App App { get; set; }
    CreateEHSRequest Model = new();
  
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid? DeliverableId { get; set; }
    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;
        Model.DeliverableId = DeliverableId;

    }

}
