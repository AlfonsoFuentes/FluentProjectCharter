using Microsoft.AspNetCore.Components;
using Shared.Models.DeliverableRisks.Requests;
using Shared.Models.Deliverables.Responses;
#nullable disable
namespace FluentWeb.Pages.DeliverableRisks;
public partial class CreateDeliverableRisk
{
    CreateDeliverableRiskRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid ScopeId { get; set; }
    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;
        Model.ScopeId = ScopeId;
    }
  
}
