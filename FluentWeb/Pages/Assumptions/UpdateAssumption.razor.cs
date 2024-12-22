using Microsoft.AspNetCore.Components;
using Shared.Models.Assumptions.Records;
using Shared.Models.Assumptions.Requests;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Projects.Records;
using Shared.Models.Projects.Reponses;
#nullable disable
namespace FluentWeb.Pages.Assumptions;
public partial class UpdateAssumption
{
    UpdateAssumptionRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<AssumptionResponse, GetAssumptionByIdRequest>(
            new GetAssumptionByIdRequest() { Id = Id,ProjectId=ProjectId });

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
