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
    public Guid DeliverableId { get; set; }
    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;
        Model.DeliverableId = DeliverableId;
    }
    private void CancelAsync()
    {
        Navigation.NavigateBack();

    }
}
