using Microsoft.AspNetCore.Components;
using Shared.Models.Deliverables.Responses;
using Shared.Models.AcceptanceCriterias.Requests;
#nullable disable
namespace FluentWeb.Pages.AcceptanceCriterias;
public partial class CreateAcceptanceCriteria
{
    CreateAcceptanceCriteriaRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid DeliverableId { get; set; }
    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;
        Model.DeliverableId = DeliverableId;
    }
   
}
