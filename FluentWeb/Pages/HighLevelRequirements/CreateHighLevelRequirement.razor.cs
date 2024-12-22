using Microsoft.AspNetCore.Components;
using Shared.Models.HighLevelRequirements.Requests;
#nullable disable
namespace FluentWeb.Pages.HighLevelRequirements;
public partial class CreateHighLevelRequirement
{
    CreateHighLevelRequirementRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }

    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;

    }
  
}
