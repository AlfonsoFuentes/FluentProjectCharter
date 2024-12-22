using Microsoft.AspNetCore.Components;
using Shared.Models.KnownRisks.Records;
using Shared.Models.KnownRisks.Requests;
using Shared.Models.KnownRisks.Responses;
#nullable disable
namespace FluentWeb.Pages.KnownRisks;
public partial class UpdateKnownRisk
{
    UpdateKnownRiskRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<KnownRiskResponse, GetKnownRiskByIdRequest>(
            new GetKnownRiskByIdRequest() { Id = Id, ProjectId= ProjectId });

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
