using Microsoft.AspNetCore.Components;
using Shared.Models.Requirements.Records;
using Shared.Models.Requirements.Requests;
using Shared.Models.Requirements.Responses;
#nullable disable
namespace FluentWeb.Pages.Requirements;
public partial class UpdateRequirement
{
    UpdateRequirementRequest Model = new();

    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<RequirementResponse, GetRequirementByIdRequest>(
            new GetRequirementByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                 ProjectId= result.Data.ProjectId,
           
            };
        }
    }
  
}
