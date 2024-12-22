using Microsoft.AspNetCore.Components;
using Shared.Models.Deliverables.Requests;
using Shared.Models.Scopes.Responses;
#nullable disable
namespace FluentWeb.Pages.Deliverables;
public partial class CreateDeliverable
{
    CreateDeliverableRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid ScopeId { get; set; }
    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;
        Model.ScopeId = ScopeId;
    }
    private void CancelAsync()
    {
        Navigation.NavigateBack();

    }
}
