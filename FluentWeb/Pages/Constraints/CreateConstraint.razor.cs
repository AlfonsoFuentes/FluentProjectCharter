using Microsoft.AspNetCore.Components;
using Shared.Models.Constrainsts.Requests;
using Shared.Models.Deliverables.Responses;
#nullable disable
namespace FluentWeb.Pages.Constraints;
public partial class CreateConstraint
{
    CreateConstrainstRequest Model = new();
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
