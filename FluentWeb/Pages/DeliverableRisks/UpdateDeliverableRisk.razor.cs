using Microsoft.AspNetCore.Components;
using Shared.Models.DeliverableRisks.Records;
using Shared.Models.DeliverableRisks.Requests;
using Shared.Models.DeliverableRisks.Responses;
#nullable disable
namespace FluentWeb.Pages.DeliverableRisks;
public partial class UpdateDeliverableRisk
{
    UpdateDeliverableRiskRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<DeliverableRiskResponse, GetDeliverableRiskByIdRequest>(
            new GetDeliverableRiskByIdRequest() { Id = Id, ProjectId= ProjectId });

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
