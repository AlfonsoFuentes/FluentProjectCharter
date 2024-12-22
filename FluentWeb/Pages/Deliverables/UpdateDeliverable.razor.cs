using Microsoft.AspNetCore.Components;
using Shared.Models.Deliverables.Records;
using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Responses;
#nullable disable
namespace FluentWeb.Pages.Deliverables;
public partial class UpdateDeliverable
{
    UpdateDeliverableRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<DeliverableResponse, GetDeliverableByIdRequest>(
            new GetDeliverableByIdRequest() { Id = Id, ProjectId= ProjectId });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = ProjectId,
            };
        }
    }
    private void CancelAsync()
    {
        Navigation.NavigateBack();

    }
}
