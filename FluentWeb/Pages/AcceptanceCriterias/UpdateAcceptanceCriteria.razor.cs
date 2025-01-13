using Microsoft.AspNetCore.Components;
using Shared.Models.AcceptanceCriterias.Records;
using Shared.Models.AcceptanceCriterias.Requests;
using Shared.Models.AcceptanceCriterias.Responses;
#nullable disable
namespace FluentWeb.Pages.AcceptanceCriterias;
public partial class UpdateAcceptanceCriteria
{
    UpdateAcceptanceCriteriaRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<AcceptanceCriteriaResponse, GetAcceptanceCriteriaByIdRequest>(
            new GetAcceptanceCriteriaByIdRequest() { Id = Id, ProjectId= ProjectId });

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
  
}
