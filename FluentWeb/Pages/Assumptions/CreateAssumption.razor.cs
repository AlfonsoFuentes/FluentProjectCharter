using Microsoft.AspNetCore.Components;
using Shared.Models.Assumptions.Requests;
#nullable disable
namespace FluentWeb.Pages.Assumptions;
public partial class CreateAssumption
{
    CreateAssumptionRequest Model = new();

    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid? ScopeId { get; set; }
    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;
        Model.ScopeId = ScopeId;
    }

}
