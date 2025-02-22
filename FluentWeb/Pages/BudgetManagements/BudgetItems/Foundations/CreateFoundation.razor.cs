using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Requests;
#nullable disable
namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Foundations;
public partial class CreateFoundation
{
    [CascadingParameter]
    public App App { get; set; }
    CreateFoundationRequest Model = new();

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
