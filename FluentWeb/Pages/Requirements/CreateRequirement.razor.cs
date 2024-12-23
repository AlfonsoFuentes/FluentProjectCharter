using Microsoft.AspNetCore.Components;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Requirements.Requests;
#nullable disable
namespace FluentWeb.Pages.Requirements;
public partial class CreateRequirement
{
    CreateRequirementRequest Model = new();
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
