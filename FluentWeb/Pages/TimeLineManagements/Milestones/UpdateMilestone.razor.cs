using Microsoft.AspNetCore.Components;
using Shared.Models.Milestones.Records;
using Shared.Models.Milestones.Requests;
using Shared.Models.Milestones.Responses;
#nullable disable
namespace FluentWeb.Pages.TimeLineManagements.Milestones;
public partial class UpdateMilestone
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public UpdateMilestoneRequest Model { get; set; } = new();   
    public Guid Id { get; set; }
   

}
