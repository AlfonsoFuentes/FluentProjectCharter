using Microsoft.AspNetCore.Components;
using Shared.Models.DecissionCriterias.Records;
using Shared.Models.DecissionCriterias.Requests;
using Shared.Models.DecissionCriterias.Responses;
#nullable disable
namespace FluentWeb.Pages.DecissionCriterias;
public partial class UpdateDecissionCriteria
{
    UpdateDecissionCriteriaRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<DecissionCriteriaResponse, GetDecissionCriteriaByIdRequest>(
            new GetDecissionCriteriaByIdRequest() { Id = Id, ProjectId= ProjectId });

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
