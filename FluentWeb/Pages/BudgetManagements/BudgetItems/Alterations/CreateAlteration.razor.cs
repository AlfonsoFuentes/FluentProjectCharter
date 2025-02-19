using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Requests;
#nullable disable
namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Alterations;
public partial class CreateAlteration
{
    [CascadingParameter]
    public App App { get; set; }
    CreateAlterationRequest Model = new();

    [Parameter]
    public Guid ProjectId { get; set; }
    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;

    }

}
