using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Structurals.Requests;
#nullable disable
namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Structurals;
public partial class CreateStructural
{
    [CascadingParameter]
    public App App { get; set; }
    CreateStructuralRequest Model = new();

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
