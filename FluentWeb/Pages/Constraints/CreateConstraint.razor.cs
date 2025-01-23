using Microsoft.AspNetCore.Components;
using Shared.Models.Constrainsts.Requests;
using Shared.Models.Deliverables.Responses;
#nullable disable
namespace FluentWeb.Pages.Constraints;
public partial class CreateConstraint
{
    CreateConstrainstRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid? ScopeId { get; set; }
    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;
        Model.ScopeId = ScopeId;
    }
   
   
}
