using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Testings.Requests;
using static Shared.StaticClasses.StaticClass;
#nullable disable
namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Testings;
public partial class CreateTesting
{
    [CascadingParameter]
    public App App { get; set; }
    CreateTestingRequest Model = new();

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
