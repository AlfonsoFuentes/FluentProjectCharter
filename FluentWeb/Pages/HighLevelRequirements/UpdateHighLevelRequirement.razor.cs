using Microsoft.AspNetCore.Components;
using Shared.Models.HighLevelRequirements.Records;
using Shared.Models.HighLevelRequirements.Requests;
using Shared.Models.HighLevelRequirements.Responses;
#nullable disable
namespace FluentWeb.Pages.HighLevelRequirements;
public partial class UpdateHighLevelRequirement
{
    UpdateHighLevelRequirementRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<HighLevelRequirementResponse, GetHighLevelRequirementByIdRequest>(new GetHighLevelRequirementByIdRequest() { Id = Id });

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
